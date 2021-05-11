[![Coverage Status](https://coveralls.io/repos/github/gonzolopez18/meli-mutant-detector/badge.svg?branch=main)](https://coveralls.io/github/gonzolopez18/meli-mutant-detector?branch=main)

# meli-mutant-detector

El objetivo de la herramienta es determinar si una secuencia de ADN pertenece a un mutante o no. 

Este servicio brinda documentaci�n a trav�s de swagger. 

Puede ver una versi�n en vivo en [MUTANT DETECTOR](https://mutantdetector.azurewebsites.net/swagger/index.html)


## Requisitos

* [Net Core SDK 3.1](https://docs.microsoft.com/en-us/dotnet/core/install/sdk?pivots=os-windows)
* [Git](https://git-scm.com/)
* [Sql Server](https://docs.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver15),
	Puede instalarlo localmente o en un servidor remoto. En cualquier caso, necesitar� contar un usuario con permisos de creaci�n de base de datos.
	Ejecutar en el servidor Sql el script que se encuentra dentro de la carpeta .ScriptBD de la soluci�n.
* [Edtitor de c�digo](https://code.visualstudio.com/)
	

## Instalaci�n  

* Clonar Repositorio

```
git clone https://github.com/gonzolopez18/meli-mutant-detector.git
``` 

* Instalar dependencias

```
cd meli-mutant-detector
dotnet restore --interactive
```

* Compilar

```
dotnet build
```

* Ejecutar tests

```
dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov
```

* Configuraci�n de base de datos
 Editar las connection string en el archivo MutantDetector.Api/appsettings.json, 
 reemplazando por los valores de la base de datos disponible. El usuario deber� poseer
 permiso de lectura y escritura sobre la misma.

*Ejecuci�n

```
dotnet run --project MutantDetector.Api
```

## API REST

Abrir https://localhost:5001/swagger/index.html




  

