# fiks.io.fagsystem.arkiv.sample

## Oppsett av prosjekt

## Bakgrunn
Dette er et forarbeid til arbeidsoppgaver i fornying av geointegrasjon for å vise muligheter og eksempler på FIKS IO integrasjon.
Flyten i meldinger baserer seg på brukstilfeller i GI-Arkiv og foreslåtte tiltak i [Sluttrapport fra arbeidsgruppe: Arkitektur og strategi](http://geointegrasjon.no/sluttrapport-fra-arbeidsgruppe-arkitektur-og-strategi/). 

Strategirapporten foreslår en inndeling av meldinger basert på erfaringer siden 2012 med GI-Arkiv og spesielt [veilederen for arkiv integrasjon](http://geointegrasjon.no/arkiv/veileder-arkiv/veileder-arkiv-for-leverandor-av-klientsystem/veileder-for-gi-arkiv-integrasjon/) som ble etablert i 2018.

- Etablere eMelding for forenklet arkivering (minimum av domenekunnskap om arkiv)
- Etablere eMelding for arkivering som er bakoverkompatibel med GI Arkiv, evt med adapter
- Etablere eMelding for arkivering med utvidet funksjonalitet

![Skisse overordnet arkitektur](ks.fiks.io.fagsystem.arkiv.sample/doc/eMeldingArkiv.png)

 ## Oppsett i FIKS Integrasjon
TBC

## FIKS IO meldingsprotokoll - Forenklet arkivering
- For fagsystemer så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.forenklet støttes som avsender
- For arkivsystem så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.forenklet støttes som mottaker
![Forenklet datamodell](ks.fiks.io.fagsystem.arkiv.sample/doc/datamodellforenklet.png)

### Meldinger fra fagsystem til arkiv
- Opprette ny saksmappe i arkivet
- Opprette en ny innkommende journalpost
- Opprette en ny utgående journalpost [no.geointegrasjon.arkiv.oppdatering.forenklet.nyutgaaendejournalpost.v2](ks.fiks.io.fagsystem.arkiv.sample/schema/no.geointegrasjon.arkiv.oppdatering.forenklet.arkivmeldingforenklet.v2.schema.json) [Eksempel json](ks.fiks.io.fagsystem.arkiv.sample/samples/utgaaendejournalpost.json)
Eksempel
```csharp
            //Fagsystem definerer ønsket struktur
            ArkivmeldingForenkletUtgaaende utg = new ArkivmeldingForenkletUtgaaende();
            utg.sluttbrukerIdentifikator = "Fagsystem";
            utg.nyUtgaaendeJournalpost = new UtgaaendeJournalpost();
            utg.nyUtgaaendeJournalpost.tittel = "Oppmålingsforretning dokument";
            utg.nyUtgaaendeJournalpost.hoveddokument = new ForenkletDokument();
            utg.nyUtgaaendeJournalpost.hoveddokument.tittel = "Rekvisisjon av oppmålingsforretning";
            utg.nyUtgaaendeJournalpost.hoveddokument.filnavn = "rekvisisjon.pdf";
            //osv...

            //Konverterer til arkivmelding xml
            var arkivmelding = Arkivintegrasjon.ConvertForenkletUtgaaendeToArkivmelding(utg);
            string payload = Arkivintegrasjon.Serialize(arkivmelding);

            //Lager FIKS IO melding
            List<IPayload> payloads = new List<IPayload>();
            payloads.Add(new StringPayload(payload, "utgaaendejournalpost.xml"));
            payloads.Add(new FilePayload(@"samples\rekvisisjon.pdf"));

            //Sender til FIKS IO (arkiv løsning)
            var msg = client.Send(messageRequest, payloads).Result;

```
Eksempel på utgaaendejournalpost.xml
```xml
<?xml version="1.0" encoding="utf-16"?>
<arkivmelding xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://www.arkivverket.no/standarder/noark5/arkivmelding">
  <system>Fagsystem</system>
  <tidspunkt>0001-01-01T00:00:00</tidspunkt>
  <antallFiler>1</antallFiler>
  <basisregistrering xsi:type="journalpost">
    <opprettetDato>0001-01-01T00:00:00</opprettetDato>
    <dokumentbeskrivelse>
      <dokumentstatus>Dokumentet er ferdigstilt</dokumentstatus>
      <opprettetDato>0001-01-01T00:00:00</opprettetDato>
      <tilknyttetRegistreringSom>Hoveddokument</tilknyttetRegistreringSom>
      <tilknyttetDato>0001-01-01T00:00:00</tilknyttetDato>
      <dokumentobjekt>
        <variantformat>Produksjonsformat</variantformat>
        <opprettetDato>0001-01-01T00:00:00</opprettetDato>
        <referanseDokumentfil>rekvisisjon.pdf</referanseDokumentfil>
      </dokumentobjekt>
    </dokumentbeskrivelse>
    <tittel>Oppmålingsforretning dokument</tittel>
    <journalposttype>Utgående dokument</journalposttype>
    <journalstatus>Journalført</journalstatus>
    <journaldato>0001-01-01</journaldato>
  </basisregistrering>
</arkivmelding>
```
- Opprette arkivnotat
- TBC

## FIKS IO meldingsprotokoll - GI bakoverkompatibel arkivering
- For fagsystemer så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.basis støttes som avsender
- For arkivsystem så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.basis støttes som mottaker

## FIKS IO meldingsprotokoll - arkivering utvidet funksjonalitet
- For fagsystemer så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.utvidet støttes som avsender
- For arkivsystem så må meldingsprotokoll no.geointegrasjon.arkiv.oppdatering.utvidet støttes som mottaker
- Denne kan feks benytte Difi eFormidling sin [arkivmelding](https://difi.github.io/felleslosninger/eformidling_nm_arkivmeldingen.html)
