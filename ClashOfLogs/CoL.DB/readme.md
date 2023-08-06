
sqllocaldb c col
sqllocaldb start col




Data Source=(localdb)\col;Initial Catalog=CoL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False



dotnet tool restore
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef


dotnet ef migrations add InitialCreate --project CoL.DB.Sqlite --startup-project CoL.Service
dotnet ef migrations add InitialCreate --project CoL.DB.Sqlite

dotnet ef migrations add UpdateName --project CoL.DB --startup-project CoL.Service
dotnet ef migrations add RenameClanMembers --project CoL.DB

dotnet ef migrations remove --project CoL.DB --startup-project CoL.Service


dotnet ef database update --project CoL.DB --startup-project CoL.Service

dotnet ef database update 0 --project CoL.DB --startup-project CoL.Service

dotnet ef database update 0 [ --context dbcontextname ]



Add-Migrationration
Remove-Migration

Update-Database

//delete db
Update-Database -Migration 0
