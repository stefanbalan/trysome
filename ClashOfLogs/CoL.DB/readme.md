
sqllocaldb c col
sqllocaldb start col




Data Source=(localdb)\col;Initial Catalog=CoL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False



dotnet tool restore

dotnet tool install --global dotnet-ef

dotnet tool update --global dotnet-ef

dotnet ef migrations add InitialCreate