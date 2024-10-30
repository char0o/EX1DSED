USE
BD_Municipalite;

CREATE TABLE municipalite
(
    code         INT PRIMARY KEY,
    nom          VARCHAR(50) NOT NULL,
    region       VARCHAR(50) NOT NULL,
    siteweb      VARCHAR(150),
    dateElection DATE,
    actif        BIT         NOT NULL,
);