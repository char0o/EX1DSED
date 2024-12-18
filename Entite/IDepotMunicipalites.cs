﻿namespace Entite;

public interface IDepotMunicipalites
{
    Municipalite? ChercherMunicipaliteParCode(int code);
    IEnumerable<Municipalite> ListerMunicipalitiesActives();
    void DesactiverMunicipalite(int code);
    void AjouterMunicipalite(Municipalite municipalite);
    void MajMunicipalite(Municipalite municipalite);
}