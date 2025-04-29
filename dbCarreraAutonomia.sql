
CREATE DATABASE IF NOT EXISTS dbCarreraAutonomia;
USE dbCarreraAutonomia;


CREATE TABLE Configuraciones (
    id INT PRIMARY KEY,
    LimiteCamisas INT NOT NULL
);


INSERT INTO Configuraciones (id, LimiteCamisas) VALUES (1, 15);


CREATE TABLE Categoria (
    idcategoria INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activa BOOLEAN DEFAULT 1
);


INSERT INTO Categoria (nombre) VALUES ('4km'), ('8km'), ('12km');


CREATE TABLE Registros (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(150) NOT NULL,
    Identificacion VARCHAR(100) NOT NULL UNIQUE,
    Correo VARCHAR(150) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    EsMayorEdad BOOLEAN NOT NULL,
    Nacionalidad VARCHAR(100) NOT NULL,
    Categoria INT,
    FechaRegistro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT fk_categoria FOREIGN KEY (Categoria) REFERENCES Categoria(idcategoria)
);


SELECT * FROM Registros;

SELECT * FROM Configuraciones;

SELECT * FROM Categoria;



SELECT 
  r.Id,
  r.NombreCompleto,
  r.Identificacion,
  r.Correo,
  r.EsMayorEdad,
  r.FechaNacimiento,
  r.FechaRegistro,
  r.Nacionalidad,
  c.nombre AS CategoriaNombre
FROM 
  Registros r
INNER JOIN 
  Categoria c ON r.Categoria = c.idcategoria;






SELECT 
  r.* 
FROM 
  Registros r
INNER JOIN 
  Categoria c ON r.Categoria = c.idcategoria
WHERE 
  c.nombre = '12km';




DELETE FROM Registros
WHERE Id = 6;

