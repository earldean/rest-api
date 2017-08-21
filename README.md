# rest-api
.Net core Web API Example

## dependencies
* Visual Studio 2017
* .Net Core framework
* Microsoft SQL Server Express 
* Postan for testing

## API endpoints 
* *GET* api/album/id - Param: artistId Returns ArtistInfo object
* *POST* api/album - Param: ArtistInfo JSON in body
* *PUT* api/album/id - Param: ArtistId, AlbumInfo JSON in body
* *DELETE* api/album/id - Param: ArtistId

* *GET* api/artist/all - Returns { *artistId*, *artistName* }
* *GET* api/albums/albums/id - Returns { [*...Listof albums*] } 

## Object examples 
**ArtistInfo**
~~~~
{
    "artistName": "STRFKR",
    "albums": [
        {
            "albumName": "Jupiter",
            "genre": "electopop",
            "year": 2009
        },
        {
            "albumName": "reptilians",
            "genre": "electropop",
            "year": 2011
        }
    ]
}
~~~~
