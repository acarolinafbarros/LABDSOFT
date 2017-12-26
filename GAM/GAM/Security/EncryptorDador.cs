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
                dador.DocIdentificacao,
                dador.IniciaisDador,
                dador.LocalNasc,
                dador.Morada,
                dador.Nacionalidade,
                dador.Nome,
                dador.Profissao
            };

            return data;
        }

        private Dador ConstructDador(IList<string> result, Dador dador)
        {
            if (result.Any())
            {
                dador.DocIdentificacao = result.ElementAt(0);
                dador.IniciaisDador = result.ElementAt(1);
                dador.LocalNasc = result.ElementAt(2);
                dador.Morada = result.ElementAt(3);
                dador.Nacionalidade = result.ElementAt(4);
                dador.Nome = result.ElementAt(5);
                dador.Profissao = result.ElementAt(6);
            }
            return dador;
        }
    }
}
