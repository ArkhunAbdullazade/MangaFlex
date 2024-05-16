cd .\MangaFlex.GateWay
start cmd /k dotnet watch run
cd .. 
cd .\MangaFlex.Identity.Api
start cmd /k dotnet watch run
cd ..
cd .\MangaFlex.Manga.Api
start cmd /k dotnet watch run
cd ..
cd .\MangaFlex.Staff.Api
start cmd /k dotnet watch run
cd ..
cd .\MangaFlex.User.Api
start cmd /k dotnet watch run
cd ..
cd .\MangaFlex.FrontEnd
start cmd /k dotnet watch run

