using FIKS.eMeldingArkiv.eMeldingForenkletArkiv;
using ks.fiks.io.fagsystem.arkiv.sample.ForenkletArkivering;
using no.ks.fiks.io.arkivmelding;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ks.fiks.io.arkivintegrasjon.tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSaksmappereferanse()
        {

            //Fagsystem definerer ønsket struktur
            ArkivmeldingForenkletInnkommende inng = new ArkivmeldingForenkletInnkommende();
            inng.sluttbrukerIdentifikator = "Fagsystemets brukerid";
            
            inng.referanseSaksmappe = new Saksmappe()
            {
                 saksaar = 2018,
                 sakssekvensnummer = 123456
            };

            inng.nyInnkommendeJournalpost = new InnkommendeJournalpost
            {
                tittel = "Tittel journalpost",
                mottattDato = DateTime.Today,
                dokumentetsDato = DateTime.Today.AddDays(-2),
                offentlighetsvurdertDato = DateTime.Today,
            };

            inng.nyInnkommendeJournalpost.referanseEksternNøkkel = new EksternNøkkel
            {
                fagsystem = "Fagsystem X",
                nøkkel = "e4712424-883c-4068-9cb7-97ac679d7232"
            };

            inng.nyInnkommendeJournalpost.internMottaker = new List<KorrespondansepartIntern>
            {
                new KorrespondansepartIntern() {
                    administrativEnhet = "Oppmålingsetaten",
                    referanseAdministrativEnhet = "b631f24b-48fb-4b5c-838e-6a1f7d56fae2"
                }
            };

            inng.nyInnkommendeJournalpost.mottaker = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Test kommune",
                    enhetsidentifikator = new Enhetsidentifikator() {
                        organisasjonsnummer = "123456789"
                    },
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Oppmålingsetaten",
                        adresselinje2 = "Rådhusgate 1",
                        postnr = "3801",
                        poststed = "Bø"
                    }
                }
            };


            inng.nyInnkommendeJournalpost.avsender = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Anita Avsender",
                    personid = new Personidentifikator() { personidentifikatorType = "F",  personidentifikatorNr = "12345678901"},
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Gate 1",
                        postnr = "3801",
                        poststed = "Bø" }
                }
            };


            inng.nyInnkommendeJournalpost.hoveddokument = new ForenkletDokument
            {
                tittel = "Rekvisisjon av oppmålingsforretning",
                filnavn = "rekvisisjon.pdf"
            };

            inng.nyInnkommendeJournalpost.vedlegg = new List<ForenkletDokument>
            {
                new ForenkletDokument(){
                    tittel = "Vedlegg 1",
                    filnavn = "vedlegg.pdf"
                }
            };


            //Konverterer til arkivmelding xml
            var arkivmelding = Arkivintegrasjon.ConvertForenkletInnkommendeToArkivmelding(inng);
            string payload = Arkivintegrasjon.Serialize(arkivmelding);

            Assert.Pass();
        }

        [Test]
        public void TestSaksmappeKlasse()
        {

            //Fagsystem definerer ønsket struktur
            ArkivmeldingForenkletInnkommende inng = new ArkivmeldingForenkletInnkommende();
            inng.sluttbrukerIdentifikator = "Fagsystemets brukerid";

            inng.referanseSaksmappe = new Saksmappe()
            {
                tittel ="Tittel mappe",
                klasse = new List<Klasse>
                { 
                    new Klasse(){ 
                        klassifikasjonssystem = "GID", 
                        klasseID = "0822-1/23" 
                    },
                    new Klasse(){
                        klassifikasjonssystem = "KK",
                        klasseID = "L3"
                    },
                },
                referanseEksternNøkkel = new EksternNøkkel
                {
                    fagsystem = "Fagsystem X",
                    nøkkel = "752f5e31-75e0-4359-bdcb-c612ba7a04eb"
                }
            };

            inng.nyInnkommendeJournalpost = new InnkommendeJournalpost
            {
                tittel = "Tittel journalpost",
                mottattDato = DateTime.Today,
                dokumentetsDato = DateTime.Today.AddDays(-2),
                offentlighetsvurdertDato = DateTime.Today,
            };

            inng.nyInnkommendeJournalpost.referanseEksternNøkkel = new EksternNøkkel
            {
                fagsystem = "Fagsystem X",
                nøkkel = "e4712424-883c-4068-9cb7-97ac679d7232"
            };

            inng.nyInnkommendeJournalpost.internMottaker = new List<KorrespondansepartIntern>
            {
                new KorrespondansepartIntern() {
                    administrativEnhet = "Oppmålingsetaten",
                    referanseAdministrativEnhet = "b631f24b-48fb-4b5c-838e-6a1f7d56fae2"
                }
            };

            inng.nyInnkommendeJournalpost.mottaker = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Test kommune",
                    enhetsidentifikator = new Enhetsidentifikator() {
                        organisasjonsnummer = "123456789"
                    },
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Oppmålingsetaten",
                        adresselinje2 = "Rådhusgate 1",
                        postnr = "3801",
                        poststed = "Bø"
                    }
                }
            };


            inng.nyInnkommendeJournalpost.avsender = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Anita Avsender",
                    personid = new Personidentifikator() { personidentifikatorType = "F",  personidentifikatorNr = "12345678901"},
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Gate 1",
                        postnr = "3801",
                        poststed = "Bø" }
                }
            };


            inng.nyInnkommendeJournalpost.hoveddokument = new ForenkletDokument
            {
                tittel = "Rekvisisjon av oppmålingsforretning",
                filnavn = "rekvisisjon.pdf"
            };

            inng.nyInnkommendeJournalpost.vedlegg = new List<ForenkletDokument>
            {
                new ForenkletDokument(){
                    tittel = "Vedlegg 1",
                    filnavn = "vedlegg.pdf"
                }
            };


            //Konverterer til arkivmelding xml
            var arkivmelding = Arkivintegrasjon.ConvertForenkletInnkommendeToArkivmelding(inng);
            string payload = Arkivintegrasjon.Serialize(arkivmelding);

            Assert.Pass();
        }

        [Test]
        public void TestSkjerming()
        {

            //Fagsystem definerer ønsket struktur
            ArkivmeldingForenkletInnkommende inng = new ArkivmeldingForenkletInnkommende();
            inng.sluttbrukerIdentifikator = "Fagsystemets brukerid";

            inng.nyInnkommendeJournalpost = new InnkommendeJournalpost
            {
                tittel = "Tittel som skal skjermes",
                mottattDato = DateTime.Today,
                dokumentetsDato = DateTime.Today.AddDays(-2),
                offentlighetsvurdertDato = DateTime.Today,
                skjermetTittel = true,
                offentligTittel = "Skjermet tittel som kan offentliggjøres",
                skjerming = new Skjerming()
                {
                     skjermingshjemmel= "Offl. § 26.1"
                }
                   
            };
            //Begrunnelse for skjerming må hjemles - Offentleglova kapittel 3 https://lovdata.no/dokument/NL/lov/2006-05-19-16/KAPITTEL_3#KAPITTEL_3

            inng.nyInnkommendeJournalpost.referanseEksternNøkkel = new EksternNøkkel
            {
                fagsystem = "Fagsystem X",
                nøkkel = "e4712424-883c-4068-9cb7-97ac679d7232"
            };

            inng.nyInnkommendeJournalpost.internMottaker = new List<KorrespondansepartIntern>
            {
                new KorrespondansepartIntern() {
                    administrativEnhet = "Oppmålingsetaten",
                    referanseAdministrativEnhet = "b631f24b-48fb-4b5c-838e-6a1f7d56fae2"
                }
            };

            inng.nyInnkommendeJournalpost.mottaker = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Test kommune",
                    enhetsidentifikator = new Enhetsidentifikator() {
                        organisasjonsnummer = "123456789"
                    },
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Oppmålingsetaten",
                        adresselinje2 = "Rådhusgate 1",
                        postnr = "3801",
                        poststed = "Bø"
                    }
                }
            };


            inng.nyInnkommendeJournalpost.avsender = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Anita Avsender",
                    skjermetKorrespondansepart = true,
                    personid = new Personidentifikator() { personidentifikatorType = "F",  personidentifikatorNr = "12345678901"},
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Gate 1",
                        postnr = "3801",
                        poststed = "Bø" }
                }
            };


            inng.nyInnkommendeJournalpost.hoveddokument = new ForenkletDokument
            {
                tittel = "Sensitiv info",
                filnavn = "brev.pdf",
                skjermetDokument = true
            };

            inng.nyInnkommendeJournalpost.vedlegg = new List<ForenkletDokument>
            {
                new ForenkletDokument(){
                    tittel = "Vedlegg 1",
                    filnavn = "vedlegg.pdf"
                }
            };

            //osv...

            //Konverterer til arkivmelding xml
            var arkivmelding = Arkivintegrasjon.ConvertForenkletInnkommendeToArkivmelding(inng);
            string payload = Arkivintegrasjon.Serialize(arkivmelding);

            Assert.Pass();
        }

        [Test]
        public void TestMapperIMappe()
        {

            //Fagsystem definerer ønsket struktur
            ArkivmeldingForenkletInnkommende inng = new ArkivmeldingForenkletInnkommende();
            inng.sluttbrukerIdentifikator = "Fagsystemets brukerid";

            inng.referanseSaksmappe = new Saksmappe()
            {
                saksaar = 2018,
                sakssekvensnummer = 123456
            };

            inng.nyInnkommendeJournalpost = new InnkommendeJournalpost
            {
                tittel = "Tittel journalpost",
                mottattDato = DateTime.Today,
                dokumentetsDato = DateTime.Today.AddDays(-2),
                offentlighetsvurdertDato = DateTime.Today
            };

            inng.nyInnkommendeJournalpost.referanseEksternNøkkel = new EksternNøkkel
            {
                fagsystem = "Fagsystem X",
                nøkkel = "e4712424-883c-4068-9cb7-97ac679d7232"
            };

            inng.nyInnkommendeJournalpost.internMottaker = new List<KorrespondansepartIntern>
            {
                new KorrespondansepartIntern() {
                    administrativEnhet = "Oppmålingsetaten",
                    referanseAdministrativEnhet = "b631f24b-48fb-4b5c-838e-6a1f7d56fae2"
                }
            };

            inng.nyInnkommendeJournalpost.mottaker = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Test kommune",
                    enhetsidentifikator = new Enhetsidentifikator() {
                        organisasjonsnummer = "123456789"
                    },
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Oppmålingsetaten",
                        adresselinje2 = "Rådhusgate 1",
                        postnr = "3801",
                        poststed = "Bø"
                    }
                }
            };


            inng.nyInnkommendeJournalpost.avsender = new List<Korrespondansepart>
            {
                new Korrespondansepart() {
                    navn = "Anita Avsender",
                    personid = new Personidentifikator() { personidentifikatorType = "F",  personidentifikatorNr = "12345678901"},
                    postadresse = new EnkelAdresse() {
                        adresselinje1 = "Gate 1",
                        postnr = "3801",
                        poststed = "Bø" }
                }
            };


            inng.nyInnkommendeJournalpost.hoveddokument = new ForenkletDokument
            {
                tittel = "Rekvisisjon av oppmålingsforretning",
                filnavn = "rekvisisjon.pdf"
            };

            inng.nyInnkommendeJournalpost.vedlegg = new List<ForenkletDokument>
            {
                new ForenkletDokument(){
                    tittel = "Vedlegg 1",
                    filnavn = "vedlegg.pdf"
                }
            };


            //Konverterer til arkivmelding xml
            var arkivmelding = Arkivintegrasjon.ConvertForenkletInnkommendeToArkivmelding(inng);

            foreach (var item in arkivmelding.Items) {
                if (item is saksmappe) {
                    ((saksmappe)item).ReferanseForeldermappe = "f3fd5a87-8703-4771-834f-5bba65df0223";
                }
            
            }

            string payload = Arkivintegrasjon.Serialize(arkivmelding);

            Assert.Pass();
        }

    }
}