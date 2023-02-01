
sqllocaldb c col
sqllocaldb start col




Data Source=(localdb)\col;Initial Catalog=Lazy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False



dotnet tool restore
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

dotnet ef migrations add InitialCreate
dotnet ef migrations add examplename --project Lazy.DB


// dotnet ef database update --project Lazy.DB

dotnet ef database update --project Lazy.DB --startup-project .\Lazy\Server\

dotnet ef database update 0 [ --context dbcontextname ]



Add-Migrationration
Remove-Migration

Update-Database

//delete db
Update-Database -Migration 0
