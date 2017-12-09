using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Security
{
    public class EncryptorDador
    {
        private IEncryptor<Dador> _dadorEncryptor;

        public EncryptorDador(IDataProtectionProvider provider)
        {
            _dadorEncryptor = new Encryptor<Dador>(provider);
        }

        public Dador EncryptData(Dador dador)
        {
            IList<string> data = ConstructDataList(dador);

            IList<string> result = _dadorEncryptor.EncryptListStrings(data);

            dador = ConstructDador(result, dador);

            return dador;
        }

        public Dador DecryptData(Dador dador)
        {
            IList<string> data = ConstructDataList(dador);

            IList<string> result = _dadorEncryptor.TryDecryptListStrings(data);

            dador = ConstructDador(result, dador);

            return dador;
        }

        public IList<Dador> DecryptDataList(IList<Dador> dadores)
        {
            IList<Dador> decryptedDadores = new List<Dador>();

            foreach(Dador dador in dadores)
            {
                IList<string> data = ConstructDataList(dador);

                IList<string> result = _dadorEncryptor.TryDecryptListStrings(data);

                decryptedDadores.Add(ConstructDador(result, dador));
            }

            return decryptedDadores;
        }

        private IList<string> ConstructDataList(Dador dador)
        {
            IList<string> data = new List<string>
            {
                dador.Altura.ToString(),
                dador.CorCabelo,
                dador.CorOlhos,
                dador.CorPele,
                dador.DataNasc.ToString(),
                dador.DocIdentificacao,
                dador.EstadoCivil.ToString(),
                dador.EstadoDador.ToString(),
                dador.Etnia,
                dador.FaseDador.ToString(),
                dador.GrauEscolaridade.ToString(),
                dador.GrupoSanguineo.ToString(),
                dador.IniciaisDador,
                dador.LocalNasc,
                dador.Morada,
                dador.Nacionalidade,
                dador.Nome,
                dador.NumAbortos.ToString(),
                dador.NumFilhos.ToString(),
                dador.Peso.ToString(),
                dador.Profissao,
                dador.TexturaCabelo,
                dador.TotalGestacoes.ToString()
            };

            return data;
        }

        private Dador ConstructDador(IList<string> result, Dador dador)
        {
            if (result.Any())
            {
                //dador.Altura = Convert.ToInt32(result.ElementAt(0));
                dador.CorCabelo = result.ElementAt(0);
                dador.CorOlhos = result.ElementAt(1);
                dador.CorPele = result.ElementAt(2);
                //dador.DataNasc = Convert.ToDateTime(result.ElementAt(4));
                dador.DocIdentificacao = result.ElementAt(3);
                //dador.EstadoCivil = (EstadoCivilEnum)Enum.Parse(typeof(EstadoCivilEnum), result.ElementAt(6));
                //dador.EstadoDador = (EstadoDadorEnum)Enum.Parse(typeof(EstadoDadorEnum), result.ElementAt(7));
                dador.Etnia = result.ElementAt(4);
                //dador.FaseDador = (FaseDadorEnum)Enum.Parse(typeof(FaseDadorEnum), result.ElementAt(9));
                //dador.GrauEscolaridade = (GrauEscolaridadeEnum)Enum.Parse(typeof(GrauEscolaridadeEnum), result.ElementAt(10));
                //dador.GrupoSanguineo = (GrupoSanguineoEnum)Enum.Parse(typeof(GrupoSanguineoEnum), result.ElementAt(11));
                dador.IniciaisDador = result.ElementAt(5);
                dador.LocalNasc = result.ElementAt(6);
                dador.Morada = result.ElementAt(7);
                dador.Nacionalidade = result.ElementAt(8);
                dador.Nome = result.ElementAt(9);
                //dador.NumAbortos = Convert.ToInt32(result.ElementAt(17));
                //dador.NumFilhos = Convert.ToInt32(result.ElementAt(18));
                //dador.Peso = float.Parse(result.ElementAt(19));
                dador.Profissao = result.ElementAt(10);
                dador.TexturaCabelo = result.ElementAt(11);
                //dador.TotalGestacoes = Convert.ToInt32(result.ElementAt(22));
            }

            return dador;
        }
    }
}
