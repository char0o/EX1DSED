﻿namespace Entite;

public class Municipalite
{
    public int Code { get; set; }
    public string Nom { get; set; }
    public string Region { get; set; }
    public string? SiteWeb { get; set; }
    public DateTime? DateElection { get; set; }

    public Municipalite(int code, string nom, string region, string? siteWeb, DateTime? dateElection)
    {
        Code = code;
        Nom = nom;
        Region = region;
        SiteWeb = siteWeb;
        DateElection = dateElection;
    }
}