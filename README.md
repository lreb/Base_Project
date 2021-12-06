# Base_Project

## Dapper and EF

Install-Package Microsoft.EntityFrameworkCore
Install-Package Dapper
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Relational
Install-Package System.Data.SqlClient
Install-Package Microsoft.EntityFrameworkCore.Tools

add-migration initial
update-database

{
    "name": "Mukesh Murugan",
    "email": "mukesh@google.es",
    "department": {
        "name" : "Development",
        "description" : "Development department"
    }
}



## references
https://codewithmukesh.com/blog/using-entity-framework-core-and-dapper/