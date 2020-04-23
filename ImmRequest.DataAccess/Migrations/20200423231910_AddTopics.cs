using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            //Topics para Espacios publicos y calles
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (1, 'Alumbrado')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (1, 'Arbolado y Plantacion')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (1, 'Equipamiento Urbano')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (1, 'Fuentes, Graffitis e Instalaciones')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (1, 'Otros')");

            //Topics para limpieza
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (2, 'Estados de los contenedores')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (2, 'Problemas de limpieza')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (2, 'Solicitud de retiro de poda, escombros o residuos de gran tamaño')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (2, 'Otros')");

            //Topics para Saneamiento
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (3, 'Bocas de tormenta')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (3, 'Obstrucciones o Perdidas')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (3, 'Otros')");

            //Topics para Trasporte
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Acoso Sexual')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Paradas')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Taxis, remixes, escolares')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Ambulancias')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Terminales')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Transporte Colectivo')");
            migrationBuilder.Sql("INSERT INTO dbo.Topics (AreaId, Name) VALUES (4, 'Otros')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.Topics");
        }
    }
}
