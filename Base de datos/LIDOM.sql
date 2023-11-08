CREATE DATABASE [LIDOM]
GO

USE [LIDOM]


CREATE TABLE [equipos] (
  [id_equipo] INT IDENTITY(1, 1) NOT NULL,
  [nombre] VARCHAR(100) NOT NULL,
  [ciudad] VARCHAR(100) NOT NULL,
  [estadio] VARCHAR(100) NOT NULL,
  CONSTRAINT [PK_equipos] PRIMARY KEY ([id_equipo])
)
GO

select * from equipos

CREATE TABLE [temporadas] (
  [id_temporada] INT IDENTITY(1, 1) NOT NULL,
  [año] INT NOT NULL,
  CONSTRAINT [PK_temporadas] PRIMARY KEY ([id_temporada])
)
GO

select * from temporadas


CREATE TABLE [partidos] (
  [id_partido] INT IDENTITY(1, 1) NOT NULL,
  [fecha] DATE NOT NULL,
  [equipo_local] INT NOT NULL,
  [equipo_visitante] INT NOT NULL,
  [resultado] VARCHAR(10) NOT NULL,
  [carreras_local] INT NOT NULL,
  [carreras_visitante] INT NOT NULL,
  [hits_local] INT NOT NULL,
  [hits_visitante] INT NOT NULL,
  [errores_local] INT NOT NULL,
  [errores_visitante] INT NOT NULL,
  [id_temporada] INT NOT NULL,
  CONSTRAINT [PK_partidos] PRIMARY KEY ([id_partido]),
  CONSTRAINT [FK_partidos_equipos_local] FOREIGN KEY ([equipo_local]) REFERENCES [equipos] ([id_equipo]),
  CONSTRAINT [FK_partidos_equipos_visitante] FOREIGN KEY ([equipo_visitante]) REFERENCES [equipos] ([id_equipo]),
  CONSTRAINT [FK_partidos_temporadas] FOREIGN KEY ([id_temporada]) REFERENCES [temporadas] ([id_temporada])
)
GO

select * from partidos

CREATE PROCEDURE ObtenerEstadisticasEquipos
AS
BEGIN
    SELECT 
        e.id_equipo AS EquipoID,
        e.nombre AS NombreEquipo,
        SUM(CASE
            WHEN p.equipo_local = e.id_equipo AND p.carreras_local > p.carreras_visitante THEN 1
            WHEN p.equipo_visitante = e.id_equipo AND p.carreras_visitante > p.carreras_local THEN 1
            ELSE 0
        END) AS JuegosGanados,
        SUM(CASE
            WHEN p.equipo_local = e.id_equipo AND p.carreras_local < p.carreras_visitante THEN 1
            WHEN p.equipo_visitante = e.id_equipo AND p.carreras_visitante < p.carreras_local THEN 1
            ELSE 0
        END) AS JuegosPerdidos,
        SUM(CASE
            WHEN p.carreras_local = p.carreras_visitante THEN 1
            ELSE 0
        END) AS JuegosEmpatados,
        SUM(
            CASE
                WHEN p.equipo_local = e.id_equipo THEN p.hits_local
                WHEN p.equipo_visitante = e.id_equipo THEN p.hits_visitante
                ELSE 0
            END
        ) AS HitsTotales,
        SUM(
            CASE
                WHEN p.equipo_local = e.id_equipo THEN p.errores_local
                WHEN p.equipo_visitante = e.id_equipo THEN p.errores_visitante
                ELSE 0
            END
        ) AS ErroresTotales,
        SUM(
            CASE
                WHEN p.equipo_local = e.id_equipo THEN p.carreras_local
                WHEN p.equipo_visitante = e.id_equipo THEN p.carreras_visitante
                ELSE 0
            END
        ) AS CarrerasTotales,
        AVG(CASE
            WHEN p.equipo_local = e.id_equipo AND p.carreras_local > p.carreras_visitante THEN 1.0
            WHEN p.equipo_visitante = e.id_equipo AND p.carreras_visitante > p.carreras_local THEN 1.0
            WHEN p.carreras_local = p.carreras_visitante THEN 0.5
            ELSE 0
        END) AS PorcentajeVictorias
    FROM equipos e
    LEFT JOIN partidos p ON e.id_equipo = p.equipo_local OR e.id_equipo = p.equipo_visitante
    GROUP BY e.id_equipo, e.nombre;
END;

EXEC ObtenerEstadisticasEquipos;








