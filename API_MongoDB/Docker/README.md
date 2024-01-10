# Mongo
Creació contenidor mongo
```bash
    cd Docker
    docker-compose up
```
# Instal·lació drivers
```bash
    dotnet add package MongoDB.Driver
```
# Autentificació base de dades admin

projecteinf@CSharp:~/Projectes/MongoDB/TestConnection$ mongosh 
Current Mongosh Log ID:	655e2b56628952b87ba939ce
Connecting to:		mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+2.0.0
Using MongoDB:		4.2.24
Using Mongosh:		2.0.0
mongosh 2.1.0 is available for download: https://www.mongodb.com/try/download/shell

For mongosh info see: https://docs.mongodb.com/mongodb-shell/

test> use admin
switched to db admin
admin> db.auth("root","a")
{ ok: 1 }
admin> 

# Creem usuari per accedir a la base de dades

booksdb> use admin
switched to db admin
admin> db.auth("root","a")
{ ok: 1 }

admin> db.createUser({user: "ubooks", pwd: "a", roles: [ { role: "readWrite", db: "booksdb" } ]})



