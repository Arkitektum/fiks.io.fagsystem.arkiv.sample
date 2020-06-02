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
        const string _mottakerKode = "EM";
        const string _avsenderKode = "EA";
        const string _internavsenderKode = "IA";
        const string _internmottakerKode = "IM";

        public static arkivmelding ConvertForenkletUtgaaendeToArkivmelding(ArkivmeldingForenkletUtgaaende input) {
            var arkivmld = new arkivmelding();
            int antFiler = 0;


            if (input.nyUtgaaendeJournalpost != null) {
                var journalpst = new journalpost();
                journalpst.tittel = input.nyUtgaaendeJournalpost.tittel;
                journalpst.journalposttype = "U";
                if (input.nyUtgaaendeJournalpost.sendtDato != null) {
                    journalpst.sendtDato = input.nyUtgaaendeJournalpost.sendtDato.Value;
                    journalpst.sendtDatoSpecified = true;
                }
                if (input.nyUtgaaendeJournalpost.dokumentetsDato != null)
                {
                    journalpst.dokumentetsDato = input.nyUtgaaendeJournalpost.dokumentetsDato.Value;
                    journalpst.dokumentetsDatoSpecified = true;
                }
                if (input.nyUtgaaendeJournalpost.offentlighetsvurdertDato != null)
                {
                    journalpst.offentlighetsvurdertDato = input.nyUtgaaendeJournalpost.offentlighetsvurdertDato.Value;
                    journalpst.offentlighetsvurdertDatoSpecified = true;
                }
                //Håndtere alle filer
                List<dokumentbeskrivelse> dokbliste = new List<dokumentbeskrivelse>();

                if (input.nyUtgaaendeJournalpost.hoveddokument != null)
                {
                    var dokbesk = new dokumentbeskrivelse();
                    dokbesk.dokumentstatus = "F";
                    dokbesk.tilknyttetRegistreringSom = "H";
                    dokbesk.tittel = input.nyUtgaaendeJournalpost.hoveddokument.tittel;

                    var dok = new dokumentobjekt();

                    dok.referanseDokumentfil = input.nyUtgaaendeJournalpost.hoveddokument.filnavn;
                    List<dokumentobjekt> dokliste = new List<dokumentobjekt>();
                    dokliste.Add(dok);

                    dokbesk.dokumentobjekt = dokliste.ToArray();

                    dokbliste.Add(dokbesk);
                    antFiler++;
                }
                foreach (var item in input.nyUtgaaendeJournalpost.vedlegg)
                {
                    var dokbesk = new dokumentbeskrivelse();
                    dokbesk.dokumentstatus = "F";
                    dokbesk.tilknyttetRegistreringSom = "V";
                    dokbesk.tittel = item.tittel;

                    var dok = new dokumentobjekt();
                    dok.referanseDokumentfil = item.filnavn;
                    List<dokumentobjekt> dokliste = new List<dokumentobjekt>();
                    dokliste.Add(dok);

                    dokbesk.dokumentobjekt = dokliste.ToArray();

                    dokbliste.Add(dokbesk);
                    antFiler++;

                }
                journalpst.dokumentbeskrivelse = dokbliste.ToArray();

                //Korrespondanseparter
                List<part> partsListe = new List<part>();

                foreach (var mottaker in input.nyUtgaaendeJournalpost.mottaker)
                {
                    part korrpart = KorrespondansepartToArkivPart(_mottakerKode, mottaker);
                    partsListe.Add(korrpart);
                }

                foreach (var avsender in input.nyUtgaaendeJournalpost.avsender)
                {
                    part korrpart = KorrespondansepartToArkivPart(_avsenderKode, avsender);
                    partsListe.Add(korrpart);
                }
                
                foreach (var internAvsender in input.nyUtgaaendeJournalpost.internAvsender)
                {
                    part korrpart = InternKorrespondansepartToArkivPart(_internavsenderKode, internAvsender);
                    partsListe.Add(korrpart);
                }

                journalpst.part = partsListe.ToArray();


                List<journalpost> jliste = new List<journalpost>();
                jliste.Add(journalpst);

                arkivmld.Items = jliste.ToArray();


            }
            arkivmld.antallFiler = antFiler;
            arkivmld.system = input.nyUtgaaendeJournalpost.referanseEksternNøkkel.fagsystem;
            arkivmld.meldingId = input.nyUtgaaendeJournalpost.referanseEksternNøkkel.nøkkel;
            arkivmld.tidspunkt = DateTime.Now;

            return arkivmld;

            
        }

        private static part KorrespondansepartToArkivPart(string partRolle, Korrespondansepart mottaker)
        {
            part korrpart = new part();
            korrpart.partNavn = mottaker.navn;
            korrpart.partRolle = partRolle;

            List<string> adresselinjer = new List<string>();
            if (mottaker.postadresse.adresselinje1 != null) adresselinjer.Add(mottaker.postadresse.adresselinje1);
            if (mottaker.postadresse.adresselinje2 != null) adresselinjer.Add(mottaker.postadresse.adresselinje2);
            if (mottaker.postadresse.adresselinje3 != null) adresselinjer.Add(mottaker.postadresse.adresselinje3);
            if (mottaker.postadresse.landkode != null) korrpart.land = mottaker.postadresse.landkode;
            if (mottaker.postadresse.postnr != null) korrpart.postnummer = mottaker.postadresse.postnr;
            if (mottaker.postadresse.poststed != null) korrpart.poststed = mottaker.postadresse.poststed;

            korrpart.postadresse = adresselinjer.ToArray();
            return korrpart;
        }
        private static part InternKorrespondansepartToArkivPart(string internavsenderKode, KorrespondansepartIntern intern)
        {
            part korrpart = new part();
            if (intern.saksbehandler != null) korrpart.partNavn = intern.saksbehandler;
            else if (intern.administrativEnhet != null) korrpart.partNavn = intern.administrativEnhet;

            korrpart.partRolle = internavsenderKode;

            
            return korrpart;
        }

        public static string Serialize(object arkivmelding)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(arkivmelding.GetType());
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, arkivmelding);
            return stringWriter.ToString();
        }

        public static object ConvertForenkletInnkommendeToArkivmelding(ArkivmeldingForenkletInnkommende input)
        {
            var arkivmld = new arkivmelding();

            int antFiler = 0;

            if (input.nyInnkommendeJournalpost != null)
            {
                var journalpst = new journalpost();
                journalpst.tittel = input.nyInnkommendeJournalpost.tittel;
                journalpst.journalposttype = "I";
                if (input.nyInnkommendeJournalpost.mottattDato != null)
                {
                    journalpst.mottattDato = input.nyInnkommendeJournalpost.mottattDato.Value;
                    journalpst.mottattDatoSpecified = true;
                }
                if (input.nyInnkommendeJournalpost.dokumentetsDato != null)
                {
                    journalpst.dokumentetsDato = input.nyInnkommendeJournalpost.dokumentetsDato.Value;
                    journalpst.dokumentetsDatoSpecified = true;
                }
                if (input.nyInnkommendeJournalpost.offentlighetsvurdertDato != null)
                {
                    journalpst.offentlighetsvurdertDato = input.nyInnkommendeJournalpost.offentlighetsvurdertDato.Value;
                    journalpst.offentlighetsvurdertDatoSpecified = true;
                }

                //Håndtere alle filer
                List<dokumentbeskrivelse> dokbliste = new List<dokumentbeskrivelse>();
                
                if (input.nyInnkommendeJournalpost.hoveddokument != null)
                {
                    var dokbesk = new dokumentbeskrivelse();
                    dokbesk.dokumentstatus = "F";
                    dokbesk.tilknyttetRegistreringSom = "H";
                    dokbesk.tittel = input.nyInnkommendeJournalpost.hoveddokument.tittel;

                    var dok = new dokumentobjekt();
                    
                    dok.referanseDokumentfil = input.nyInnkommendeJournalpost.hoveddokument.filnavn;
                    List<dokumentobjekt> dokliste = new List<dokumentobjekt>();
                    dokliste.Add(dok);

                    dokbesk.dokumentobjekt = dokliste.ToArray();
                    
                    dokbliste.Add(dokbesk);
                    antFiler++;
                }
                foreach (var item in input.nyInnkommendeJournalpost.vedlegg)
                {
                    var dokbesk = new dokumentbeskrivelse();
                    dokbesk.dokumentstatus = "F";
                    dokbesk.tilknyttetRegistreringSom = "V";
                    dokbesk.tittel = item.tittel;

                    var dok = new dokumentobjekt();
                    dok.referanseDokumentfil = item.filnavn;
                    List<dokumentobjekt> dokliste = new List<dokumentobjekt>();
                    dokliste.Add(dok);

                    dokbesk.dokumentobjekt = dokliste.ToArray();
                    
                    dokbliste.Add(dokbesk);
                    antFiler++;

                }
                journalpst.dokumentbeskrivelse = dokbliste.ToArray();

                //Korrespondanseparter
                List<part> partsListe = new List<part>();

                foreach (var mottaker in input.nyInnkommendeJournalpost.mottaker)
                {
                    part korrpart = KorrespondansepartToArkivPart(_mottakerKode, mottaker);
                    partsListe.Add(korrpart);
                }

                foreach (var avsender in input.nyInnkommendeJournalpost.avsender)
                {
                    part korrpart = KorrespondansepartToArkivPart(_avsenderKode, avsender);
                    partsListe.Add(korrpart);
                }

                foreach (var internMottaker in input.nyInnkommendeJournalpost.internMottaker)
                {
                    part korrpart = InternKorrespondansepartToArkivPart(_internmottakerKode, internMottaker);
                    partsListe.Add(korrpart);
                }

                journalpst.part = partsListe.ToArray();


                List<journalpost> jliste = new List<journalpost>();
                jliste.Add(journalpst);


                arkivmld.Items = jliste.ToArray();

            }
            arkivmld.antallFiler = antFiler;
            arkivmld.system = input.nyInnkommendeJournalpost.referanseEksternNøkkel.fagsystem;
            arkivmld.meldingId = input.nyInnkommendeJournalpost.referanseEksternNøkkel.nøkkel;
            arkivmld.tidspunkt = DateTime.Now;

            return arkivmld;
        }
    }
}
