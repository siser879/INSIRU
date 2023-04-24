CREATE DATABASE Insiru;
USE Insiru;

CREATE TABLE Pokemon (
    id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(25),
    Tipo NVARCHAR(25),
    Vida INT
);

INSERT INTO Pokemon (Nombre, Tipo, Vida) 
VALUES 
('Charmander', 'Fuego', 39),
('Bulbasaur', 'Planta', 45),
('Squirtle', 'Agua', 44);

CREATE TABLE Tipo_ataque (
    id INT IDENTITY(1,1) PRIMARY KEY,
    Tipo VARCHAR(30)
);

INSERT INTO Tipo_ataque (Tipo)
VALUES 
('Ofensivo'),
('Defensivo'),
('Curacion'),
('Generico');

CREATE TABLE Ataque (
    id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(30),
    tipo INT,
    FOREIGN KEY (tipo) REFERENCES Tipo_ataque(id)
);

INSERT INTO Ataque (Nombre, tipo)
VALUES 
('Placaje', 1),
('Bloqueo', 2),
('Curar', 3),
('Elemental', 4);

CREATE TABLE Estadisticas (
    Id_pokemon INT PRIMARY KEY,
    numVeces_elegido INT DEFAULT 0,
    numVeces_ganado INT DEFAULT 0,
    numVeces_perdido INT DEFAULT 0,
    numVeces_usadoPlacaje INT DEFAULT 0,
    numVeces_usadoBloqueo INT DEFAULT 0,
    numVeces_usadoCurar INT DEFAULT 0,
    numVeces_usadoElemental INT DEFAULT 0,
    FOREIGN KEY (Id_pokemon) REFERENCES Pokemon(id)
);

INSERT INTO Estadisticas (Id_pokemon)
VALUES 
  (1),
  (2),
  (3);