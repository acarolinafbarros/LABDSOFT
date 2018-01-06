using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GAM.Models;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using GAM.Models.PMA;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace GAM.Helpers
{
    public static class MatchHelper
    {
        /// <summary>
        /// Converte Grupo sanguineo do casal (homem + mulher)
        /// </summary>
        /// <param name="casal">Casal</param>
        /// <returns>Lista de grupos sanguineos ordenada</returns>
        public static List<GrupoSanguineoMatchEnum> ConvertGrupoSanguineo(this Casal casal)
        {
            return new List<GrupoSanguineoMatchEnum>{
                casal.GrupoSanguineoMulher.ConvertGrupoSanguineo(),
                casal.GrupoSanguineoHomem.ConvertGrupoSanguineo()
            }.OrderBy(x=>(int)x).ToList();
        }

        /// <summary>
        /// Converte grupo sanguineo para grupo sanguineo Base (utilizado para a tabela de exclusao por grupos sanguineos)
        /// </summary>
        /// <param name="gs">Grupo sanguineo a converter</param>
        /// <returns>Grupo sanguineo apenas A, B, AB e O</returns>
        public static GrupoSanguineoMatchEnum ConvertGrupoSanguineo(this GrupoSanguineoEnum gs)
        {
            if (gs == GrupoSanguineoEnum.APos || gs == GrupoSanguineoEnum.ANeg)
                return GrupoSanguineoMatchEnum.A;
            if (gs == GrupoSanguineoEnum.BPos || gs == GrupoSanguineoEnum.BNeg)
                return GrupoSanguineoMatchEnum.B;
            if (gs == GrupoSanguineoEnum.ABPos || gs == GrupoSanguineoEnum.ABNeg)
                return GrupoSanguineoMatchEnum.AB;

            return GrupoSanguineoMatchEnum.O;
        }

        /// <summary>
        /// Verifica compatibilidade segundo tabela ABO
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool VerificaCompatibilidade_Abo(Casal casal, Dador dador)
        {
            var gruposCasal = casal.ConvertGrupoSanguineo();
            if (gruposCasal.Contains(GrupoSanguineoMatchEnum.AB))
                return true;

            //A+B
            if (gruposCasal.Contains(GrupoSanguineoMatchEnum.A) && gruposCasal.Contains(GrupoSanguineoMatchEnum.B))
                return true;

            //Dador == O
            if (dador.GrupoSanguineo.ConvertGrupoSanguineo()==GrupoSanguineoMatchEnum.O)
                return true;

            if (gruposCasal.Contains(dador.GrupoSanguineo.ConvertGrupoSanguineo()))
                return true;


            return false;
        }

        /// <summary>
        /// Verifica compatibilidade de RH
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool VerificaCompatibilidade_Rh(Casal casal, Dador dador)
        {
            //Sintonia entre casal e diferença do dador provoca exclusao
            if (((int) casal.GrupoSanguineoMulher) % 2 == ((int) casal.GrupoSanguineoMulher) % 2 &&
                ((int) casal.GrupoSanguineoMulher) % 2 != ((int) dador.GrupoSanguineo) % 2)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifica compatibilidade de olhos
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool VerificaCompatibilidade_Olhos(Casal casal, Dador dador)
        {
            //TODO faltam enums
            return true;
        }

        /// <summary>
        /// Valida se existe compatibilidade entre casal e dador, 
        /// utilizando o factor de exclusao (ABO, RH e Olhos)
        /// </summary>
        /// <param name="casal">Casal a verificar</param>
        /// <param name="dador">Dador a verificar</param>
        /// <returns>Compatibilidade possivel entre dador e casal</returns>
        public static bool GamMatch(this Casal casal, Dador dador)
        {
            //mecanismo de match
            
            return VerificaCompatibilidade_Rh(casal, dador) 
                && VerificaCompatibilidade_Abo(casal, dador)
                && VerificaCompatibilidade_Olhos(casal, dador);
        }

        /// <summary>
        /// Ordena lista segundo uma preferencia MatchStatsInfo
        /// </summary>
        /// <param name="dadors">lista de dadores (compativeis)</param>
        /// <param name="casal">casal selecionado</param>
        /// <param name="stats">dados preferenciais</param>
        /// <returns>Lista ordenada de compatibilidades</returns>
        public static List<Dador> GetOrdedList(List<Dador> dadors, Casal casal, MatchStatsInfo stats)
        {
            var listaComparacao = dadors.Select(x => new
            {
                x.DadorId,
                GrupoSanguineoMulher = casal.GrupoSanguineoMulher == x.GrupoSanguineo,
                GrupoSanguineoHomem = casal.GrupoSanguineoHomem == x.GrupoSanguineo,
                CorCabeloHomem = casal.CorCabeloHomem == x.CorCabelo,
                CorCabeloMulher = casal.CorCabeloMulher == x.CorCabelo,
                CorOlhosHomem = casal.CorOlhosHomem == x.CorOlhos,
                CorOlhosMulher = casal.CorOlhosMulher == x.CorOlhos,
                CorPeleHomem = casal.CorPeleHomem == x.CorPele,
                CorPeleMulher = casal.CorPeleMulher == x.CorPele,
                RacaHomem = casal.RacaHomem == x.Etnia,
                RacaMulher = casal.RacaHomem == x.Etnia,
                TexturaCabeloHomem = casal.TexturaCabeloHomem == x.TexturaCabelo,
                TexturaCabeloMulher = casal.TexturaCabeloMulher == x.TexturaCabelo

                //DUMMY
                //CorCabeloHomem =false,
                //CorCabeloMulher = false,
                //CorOlhosHomem = false,
                //CorOlhosMulher = false,
                //CorPeleHomem = false,
                //CorPeleMulher = false,
                //RacaHomem = false,
                //RacaMulher = false,
                //TexturaCabeloHomem = false,
                //TexturaCabeloMulher = false

            });

            var ordemStats = stats.GetType().GetProperties().Select(x => new
            {
                Field = x.Name,
                Value = x.GetValue(stats, null)
            }).OrderBy(x=>x.Value).Select(x=>x.Field).ToList().Aggregate((x,y)=> $"{x},{y}" );
            var comparacaoOrdenada = System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(listaComparacao.AsQueryable(), ordemStats);

            //var comparacaoOrdenada = listaComparacao.OrderBy(ordemStats);
            var sortedList = comparacaoOrdenada.Select(x => dadors.FirstOrDefault(y => x.DadorId == y.DadorId)).ToList();

            return sortedList;
        }

        /// <summary>
        /// Devolve um MatchStats, obtido atravez da comparacao entre dador e casal,
        /// detalhando as semelhancas entre ambos
        /// </summary>
        /// <param name="casal">casal selecionado</param>
        /// <param name="dador">dador a comparar</param>
        /// <returns>MatchStats, com informação sobre match entre dador e casal</returns>
        public static MatchStats GetMatchStats(Casal casal, Dador dador)
        {
            var matchStats = new MatchStats
            {
                GrupoSanguineoMulher = casal.GrupoSanguineoMulher==dador.GrupoSanguineo,
                GrupoSanguineoHomem = casal.GrupoSanguineoHomem == dador.GrupoSanguineo,
                CorCabeloHomem = casal.CorCabeloHomem == dador.CorCabelo,
                CorCabeloMulher = casal.CorCabeloMulher == dador.CorCabelo,
                CorOlhosHomem = casal.CorOlhosHomem == dador.CorOlhos,
                CorOlhosMulher = casal.CorOlhosMulher == dador.CorOlhos,
                CorPeleHomem = casal.CorPeleHomem == dador.CorPele,
                CorPeleMulher = casal.CorPeleMulher == dador.CorPele,
                RacaHomem = casal.RacaHomem == dador.Etnia,
                RacaMulher = casal.RacaHomem == dador.Etnia,
                TexturaCabeloHomem  = casal.TexturaCabeloHomem == dador.TexturaCabelo,
                TexturaCabeloMulher = casal.TexturaCabeloMulher == dador.TexturaCabelo,
                CasalId = casal.CasalID,
                DadorId = dador.DadorId,
                

            };

            return matchStats;
        }


        public class MicrosoftCognitiveServices
        {
            public class Faces
            {
                private static readonly IFaceServiceClient faceServiceClient =
                    new FaceServiceClient("4d2911d970fe4383a8fbdf76a86dca06", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

                private const string faceList = "igam_2018";

                public static async Task CreateFaceList()
                {
                    await faceServiceClient.CreateFaceListAsync(faceList, faceList, faceList);
                }

                public static async Task<Guid?> AddFaceToFaceList(string imageFilePath, string FaceId)
                {

                    try
                    {
                        using (Stream imageFileStream = File.OpenRead(imageFilePath))
                        {
                            var result = await faceServiceClient.AddFaceToFaceListAsync(faceList,imageFileStream, FaceId);

                            return result.PersistedFaceId;
                                
                        }
                    }
                    // Catch and display Face API errors.
                    catch (FaceAPIException f)
                    {

                        // MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                        return null;
                    }
                    // Catch and display all other errors.
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message, "Error");
                        return null;
                    }

                }

                public static async Task<SimilarPersistedFace[]> TestFindSimilars(string imageFilePath, string FaceId, string filePathBase)
                {
                    string[] files = Directory.GetFiles(filePathBase, "*.jpg", SearchOption.AllDirectories);
                    await faceServiceClient.DeleteFaceListAsync(faceList);
                    await faceServiceClient.CreateFaceListAsync(faceList, faceList, faceList);

                    foreach (string s in files)
                    {
                        var fileName = s.Split('\\').Last();
                        
                        await AddFaceToFaceList(s, fileName);
                    }

                    try
                    {
                        using (Stream imageFileStream = File.OpenRead(imageFilePath))
                        {
                            IEnumerable<FaceAttributeType> faceAttributes =
                                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses };
                            Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                            if (faces.Any())
                            {
                                var result = await faceServiceClient.FindSimilarAsync(faces[0].FaceId, faceList, 10);

                                //await faceServiceClient.DeleteFaceFromFaceListAsync(faceList, faces[0]);
                                return result;
                            }
                        }
                    }
                    // Catch and display Face API errors.
                    catch (FaceAPIException f)
                    {

                        // MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                        return null;
                    }
                    // Catch and display all other errors.
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message, "Error");
                        return null;
                    }
                    return null;
                }

                public static async Task<Tuple<List<SimilarPersistedFace>, string>> FindSimilarFaceList(string imageFilePath, string FaceId)
                {

                    try
                    {
                        using (Stream imageFileStream = File.OpenRead(imageFilePath))
                        {
                            IEnumerable<FaceAttributeType> faceAttributes =
                                new FaceAttributeType[]
                                {
                                    FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile,
                                    FaceAttributeType.Glasses
                                };
                            Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true,
                                returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                            if (faces.Any())
                            {
                                var result = await faceServiceClient.FindSimilarAsync(faces[0].FaceId, faceList, 10);

                                //await faceServiceClient.DeleteFaceFromFaceListAsync(faceList, faces[0]);
                                if (result.Any())
                                {
                                    result.OrderBy(x => x.Confidence);
                                }
                            }
                        }
                    }
                    // Catch and display Face API errors.
                    catch (FaceAPIException f)
                    {

                        // MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                        return new Tuple<List<SimilarPersistedFace>, string>(new List<SimilarPersistedFace>(), f.ErrorMessage); 
                    }
                    // Catch and display all other errors.
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message, "Error");
                        return new Tuple<List<SimilarPersistedFace>, string>(new List<SimilarPersistedFace>(), e.Message);
                    }
                    return new Tuple<List<SimilarPersistedFace>, string>(new List<SimilarPersistedFace>(), "NoFound");
                }


                //private async Task<Face[]> UploadAndDetectFaces(string imageFilePath)
                //{
                //    // The list of Face attributes to return.
                //    IEnumerable<FaceAttributeType> faceAttributes =
                //        new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses};

                //    // Call the Face API.
                //    try
                //    {
                //        using (Stream imageFileStream = File.OpenRead(imageFilePath))
                //        {
                //            faceServiceClient. AddFaceToFaceListAsync()
                //            Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                //            return faces;
                //        }
                //    }
                //    // Catch and display Face API errors.
                //    catch (FaceAPIException f)
                //    {

                //       // MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                //        return new Face[0];
                //    }
                //    // Catch and display all other errors.
                //    catch (Exception e)
                //    {
                //        //MessageBox.Show(e.Message, "Error");
                //        return new Face[0];
                //    }
                //}


                public const string apiKey = "4d2911d970fe4383a8fbdf76a86dca06";

                static async void MakeRequest()
                {
                    
                    //var server = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0";

                    var client = new HttpClient();
                    var queryString = HttpUtility.ParseQueryString(string.Empty);

                    // Request headers
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

                    //var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/facelists/{faceListId}?" + queryString;
                    var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/facelists/casais?" + queryString;

                    HttpResponseMessage response;

                    // Request body
                    byte[] byteData = Encoding.UTF8.GetBytes("{body}");

                    using (var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                        response = await client.PutAsync(uri, content);
                    }
                }
            }

            //static async void MakeRequest()
            //{
            //    var client = new HttpClient();
            //    var queryString = HttpUtility.ParseQueryString(string.Empty);

            //    // Request headers
            //    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            //    // Request parameters
            //    queryString["userData"] = "{string}";
            //    queryString["targetFace"] = "{string}";
            //    var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/facelists/casais/persistedFaces?" + queryString;

            //    HttpResponseMessage response;

            //    // Request body
            //    byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            //    using (var content = new ByteArrayContent(byteData))
            //    {
            //        content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");

            //        response = await client.PostAsync(uri, content);
            //    }

            //}
        }



    }
}
