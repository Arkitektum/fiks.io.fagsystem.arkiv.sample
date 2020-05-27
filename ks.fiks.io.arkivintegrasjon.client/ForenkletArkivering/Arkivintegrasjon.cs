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
            //TODO mapping
            

            if (input.nyUtgaaendeJournalpost != null) {
                var journalpst = new journalpost();
                journalpst.tittel = input.nyUtgaaendeJournalpost.tittel;
                journalpst.journalposttype = "U";

                if (input.nyUtgaaendeJournalpost.hoveddokument != null)
                {
                    var dokbesk = new dokumentbeskrivelse();
                    dokbesk.dokumentstatus = "F";
                    dokbesk.tilknyttetRegistreringSom = "H";
                   
                    var dok = new dokumentobjekt();
                    dok.referanseDokumentfil = input.nyUtgaaendeJournalpost.hoveddokument.filnavn;
                    List<dokumentobjekt> dokliste = new List<dokumentobjekt>();
                    dokliste.Add(dok);

                    dokbesk.dokumentobjekt = dokliste.ToArray();
                    List<dokumentbeskrivelse> dokbliste = new List<dokumentbeskrivelse>();
                    dokbliste.Add(dokbesk);

                    journalpst.dokumentbeskrivelse = dokbliste.ToArray();
                }
                List<journalpost> jliste = new List<journalpost>();
                jliste.Add(journalpst);
                arkivmld.Items = jliste.ToArray();

            }
            arkivmld.antallFiler = 1;
            arkivmld.system = input.sluttbrukerIdentifikator;
            arkivmld.tidspunkt = DateTime.Now;

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
