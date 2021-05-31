create database PruebaFotos

use PruebaFotos



create table Imagenes(
    Id INT PRIMARY KEY IDENTITY (1, 1),
    Imagen image,
    Titulo varchar(50),

);



select * 
from Imagenes

							   