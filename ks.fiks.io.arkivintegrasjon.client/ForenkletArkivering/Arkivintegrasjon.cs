using FIKS.eMeldingArkiv.eMeldingForenkletArkiv;
using no.ks.fiks.io.arkivmelding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ks.fiks.io.fagsystem.arkiv.sample.ForenkletArkivering
{
    public class Arkivintegrasjon
    {
        public static arkivmelding ConvertForenkletUtgaaendeToArkivmelding(ArkivmeldingForenkletUtgaaende input) {
            var arkivmld = new arkivmelding();
            arkivmld.system = input.sluttbrukerIdentifikator; //ikke riktig men...
            if (input.nyUtgaaendeJournalpost != null) {
                


            }
            //TODO mapping
            return arkivmld;

        }


        public static string Serialize(object arkivmelding)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(arkivmelding.GetType());
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, arkivmelding);
            return stringWriter.ToString();
        }

    }
}
