using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintingHouse.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(10, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "orderCatagory",
                columns: table => new
                {
                    orderCatagoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderCatagory", x => x.orderCatagoryID);
                });

            migrationBuilder.CreateTable(
                name: "orderType",
                columns: table => new
                {
                    orderTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderType", x => x.orderTypeID);
                });

            migrationBuilder.CreateTable(
                name: "prodductService",
                columns: table => new
                {
                    prodductService = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productID = table.Column<int>(nullable: true),
                    ServiceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__prodduct__EDEFB117EB6EAC7C", x => x.prodductService);
                });

            migrationBuilder.CreateTable(
                name: "proPerty",
                columns: table => new
                {
                    proPertyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proPerty", x => x.proPertyID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "size",
                columns: table => new
                {
                    sizeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_size", x => x.sizeID);
                });

            migrationBuilder.CreateTable(
                name: "suplier",
                columns: table => new
                {
                    suplierID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(10, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suplier", x => x.suplierID);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    orderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    size = table.Column<double>(nullable: true),
                    customerID = table.Column<int>(nullable: true),
                    orderTypeID = table.Column<int>(nullable: true),
                    orderDeadline = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.orderID);
                    table.ForeignKey(
                        name: "FK__order__customerI__6E01572D",
                        column: x => x.customerID,
                        principalTable: "customer",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__orderType__6EF57B66",
                        column: x => x.orderTypeID,
                        principalTable: "orderType",
                        principalColumn: "orderTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    propertyID = table.Column<int>(nullable: true),
                    orderSizeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productID);
                    table.ForeignKey(
                        name: "FK__product__propert__22751F6C",
                        column: x => x.propertyID,
                        principalTable: "proPerty",
                        principalColumn: "proPertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    paymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderID = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10, 2)", nullable: true),
                    payedAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: true),
                    paymentDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK__payment__orderID__72C60C4A",
                        column: x => x.orderID,
                        principalTable: "order",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productProperty",
                columns: table => new
                {
                    productPropertyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productID = table.Column<int>(nullable: true),
                    proPertyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productProperty", x => x.productPropertyID);
                    table.ForeignKey(
                        name: "FK__productPr__proPe__2645B050",
                        column: x => x.proPertyID,
                        principalTable: "proPerty",
                        principalColumn: "proPertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__productPr__produ__25518C17",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "productID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "productSize",
                columns: table => new
                {
                    productSizeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productID = table.Column<int>(nullable: true),
                    sizeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSize", x => x.productSizeID);
                    table.ForeignKey(
                        name: "FK__productSi__produ__245D67DE",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "productID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderTransaction",
                columns: table => new
                {
                    orderTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    orderID = table.Column<int>(nullable: true),
                    paymentID = table.Column<int>(nullable: true),
                    suplierID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderTransaction", x => x.orderTransactionID);
                    table.ForeignKey(
                        name: "FK__orderTran__order__6FE99F9F",
                        column: x => x.orderID,
                        principalTable: "order",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__orderTran__payme__70DDC3D8",
                        column: x => x.paymentID,
                        principalTable: "payment",
                        principalColumn: "paymentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__orderTran__supli__71D1E811",
                        column: x => x.suplierID,
                        principalTable: "suplier",
                        principalColumn: "suplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_customerID",
                table: "order",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_order_orderTypeID",
                table: "order",
                column: "orderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_orderTransaction_orderID",
                table: "orderTransaction",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_orderTransaction_paymentID",
                table: "orderTransaction",
                column: "paymentID");

            migrationBuilder.CreateIndex(
                name: "IX_orderTransaction_suplierID",
                table: "orderTransaction",
                column: "suplierID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_orderID",
                table: "payment",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_product_propertyID",
                table: "product",
                column: "propertyID");

            migrationBuilder.CreateIndex(
                name: "IX_productProperty_proPertyID",
                table: "productProperty",
                column: "proPertyID");

            migrationBuilder.CreateIndex(
                name: "IX_productProperty_productID",
                table: "productProperty",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_productSize_productID",
                table: "productSize",
                column: "productID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderCatagory");

            migrationBuilder.DropTable(
                name: "orderTransaction");

            migrationBuilder.DropTable(
                name: "prodductService");

            migrationBuilder.DropTable(
                name: "productProperty");

            migrationBuilder.DropTable(
                name: "productSize");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "size");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "suplier");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "proPerty");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "orderType");
        }
    }
}
