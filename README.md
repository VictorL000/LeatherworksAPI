# installation
```powershell
docker run --name leatherworksSQL -e POSTGRES_PASSWORD=leatherworking -v sqlvolume:/var/opt/postgres --rm -d postgres -p 1433
```