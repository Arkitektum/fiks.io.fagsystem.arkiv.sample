///////////////////////////////////////////////////////////
//  Saksmappe.cs
//  Implementation of the Class Saksmappe
//  Generated by Enterprise Architect
//  Created on:      27-mai-2020 09:37:05
//  Original author: TorKjetilNilsen
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using GeoIntegrasjon.BasicTypes;
using FIKS.eMeldingArkiv.eMeldingForenkletArkiv;
namespace FIKS.eMeldingArkiv.eMeldingForenkletArkiv {
	public class Saksmappe {

		/// <summary>
		/// Definisjon: Inng�r i M003 mappeID. Viser �ret saksmappen ble opprettet.
		/// 
		/// Kilde: Registreres automatisk n�r saksmappen opprettes
		/// 
		/// Kommentar: Se kommentar under M012 sakssekvensnummer
		/// 
		/// M011 saksaar
		/// </summary>
		public GeoIntegrasjon.BasicTypes.integer saksaar;
		/// <summary>
		/// Definisjon: Inng�r i M003 mappeID. Viser rekkef�lgen n�r saksmappen ble
		/// opprettet innenfor �ret.
		/// 
		/// Kilde: Registreres automatisk n�r saksmappen opprettes
		/// 
		/// Kommentar: Kombinasjonen saks�r og sakssekvensnummer er ikke obligatorisk, men
		/// anbefales brukt i sakarkiver.
		/// 
		/// M012 sakssekvensnummer
		/// </summary>
		public GeoIntegrasjon.BasicTypes.integer sakssekvensnummer;
		/// <summary>
		/// angir mappetype som blant annet kan brukes som hint til hva som ligger i
		/// virksomhetsspesifikkemetadata
		/// </summary>
		public Mappetype mappetype;
		/// <summary>
		/// Definisjon: Datoen saken er opprettet
		/// 
		/// Kilde: Settes automatisk til samme dato som M600 opprettetDato
		/// 
		/// Kommentar: (ingen)
		/// 
		/// M100 saksdato
		/// </summary>
		public date saksdato;
		/// <summary>
		/// Definisjon: Tittel eller navn p� arkivenheten
		/// 
		/// Kilde: Registreres manuelt eller hentes automatisk fra innholdet i
		/// arkivdokumentet. Ja fra klassetittel dersom alle mapper skal ha samme tittel
		/// som klassen. Kan ogs� hentes automatisk fra et fagsystem.
		/// 
		/// Kommentarer: For saksmappe og journalpost vil dette tilsvare "Sakstittel" og
		/// "Dokumentbeskrivelse". Disse navnene kan beholdes i grensesnittet.
		/// 
		/// M020
		/// </summary>
		public GeoIntegrasjon.BasicTypes.string tittel;
		/// <summary>
		/// Definisjon: Navn p� avdeling, kontor eller annen administrativ enhet som har
		/// ansvaret for saksbehandlingen.
		/// 
		/// Kilde: Registreres automatisk f.eks. p� grunnlag av innlogget bruker, kan
		/// overstyres
		/// 
		/// Kommentar: Merk at p� journalpostniv� grupperes administrativEnhet sammen med
		/// M307 saksbehandler inn i korrespondansepart. Dette muliggj�r individuell
		/// behandling n�r det er flere mottakere, noe som er s�rlig aktuelt ved
		/// organinterne dokumenter som skal f�lges opp.
		/// 
		/// M305 administrativEnhet
		/// </summary>
		public GeoIntegrasjon.BasicTypes.string administrativEnhet;
		/// <summary>
		/// Definisjon: Offentlig tittel p� arkivenheten, ord som skal skjermes er fjernet
		/// fra innholdet i tittelen (erstattet med ******)
		/// 
		/// Kommentarer: I l�pende og offentlig journaler skal ogs� offentligTittel v�re
		/// med dersom ord i tittelfeltet skal skjermes.
		/// 
		/// M025
		/// </summary>
		public GeoIntegrasjon.BasicTypes.string offentligTittel;
		public SystemID referanseAdministrativEnhet;
		/// <summary>
		/// Definisjon: Navn p� person som er saksansvarlig
		/// 
		/// Kilde: Registreres automatisk p� grunnlag av innlogget bruker eller annen
		/// saksbehandlingsfunksjonalitet (f.eks. saksfordeling), kan overstyres manuelt
		/// 
		/// Kommentar: (ingen)
		/// 
		/// M306 saksansvarlig
		/// </summary>
		public GeoIntegrasjon.BasicTypes.string saksansvarlig;
		public SystemID referanseSaksansvarlig;
		/// <summary>
		/// Definisjon: Status til saksmappen, dvs. hvor langt saksbehandlingen har kommet.
		/// 
		/// 
		/// Kilde: Registreres automatisk gjennom forskjellig saksbehandlings-
		/// funksjonalitet, eller overstyres manuelt.
		/// 
		/// Kommentar: Saksmapper som avleveres skal ha status "Avsluttet" eller "Utg�r".
		/// 
		/// M052 saksstatus
		/// </summary>
		public Saksstatus saksstatus;
		/// <summary>
		/// Definisjon: Navn p� person som avsluttet/lukket arkivenheten
		/// 
		/// Kilde: Registreres automatisk av systemet ved opprettelse av enheten
		/// 
		/// Kommentarer: (ingen)
		/// 
		/// M603
		/// </summary>
		public GeoIntegrasjon.BasicTypes.string avsluttetAv;
		/// <summary>
		/// saU1 i n4
		/// </summary>
		public boolean skjermetTittel;
		public EksternN�kkel referanseEksternN�kkel;

		public Saksmappe(){

		}

		~Saksmappe(){

		}

	}//end Saksmappe

}//end namespace eMeldingForenkletArkiv