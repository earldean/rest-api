
use Artist;

create table dbo.Artists 
(ArtistId int Identity(1,1) primary key not null,
ArtistName nvarchar(128) not null)