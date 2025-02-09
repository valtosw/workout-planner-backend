using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutPlanner.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfilePicture = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Experience = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceOfWork = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainingPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    InstagramLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FacebookLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelegramLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainerCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerCustomer", x => new { x.CustomerId, x.TrainerId });
                    table.ForeignKey(
                        name: "FK_TrainerCustomer_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerCustomer_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainerRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerRequests_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerRequests_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedById = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssignedToId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainerId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_AspNetUsers_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_MuscleGroups_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalTable: "MuscleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProgressLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressLogs_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressLogs_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutPlanEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlanEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanEntries_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanEntries_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "MuscleGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "abdominals" },
                    { 2, "adductors" },
                    { 3, "quadriceps" },
                    { 4, "biceps" },
                    { 5, "shoulders" },
                    { 6, "chest" },
                    { 7, "hamstrings" },
                    { 8, "middle back" },
                    { 9, "calves" },
                    { 10, "glutes" },
                    { 11, "lower back" },
                    { 12, "lats" },
                    { 13, "triceps" },
                    { 14, "traps" },
                    { 15, "forearms" },
                    { 16, "neck" },
                    { 17, "abductors" },
                    { 18, "" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "MuscleGroupId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "ab crunch machine" },
                    { 2, 1, "ab roller" },
                    { 3, 2, "adductor" },
                    { 4, 2, "adductor/groin" },
                    { 5, 1, "advanced kettlebell windmill" },
                    { 6, 1, "air bike" },
                    { 7, 3, "all fours quad stretch" },
                    { 8, 4, "alternate hammer curl" },
                    { 9, 1, "alternate heel touchers" },
                    { 10, 4, "alternate incline dumbbell curl" },
                    { 11, 3, "alternate leg diagonal bound" },
                    { 12, 5, "alternating cable shoulder press" },
                    { 13, 5, "alternating deltoid raise" },
                    { 14, 6, "alternating floor press" },
                    { 15, 7, "alternating hang clean" },
                    { 16, 5, "alternating kettlebell press" },
                    { 17, 8, "alternating kettlebell row" },
                    { 18, 8, "alternating renegade row" },
                    { 19, 9, "ankle circles" },
                    { 20, 10, "ankle on the knee" },
                    { 21, 9, "anterior tibialis-smr" },
                    { 22, 5, "anti-gravity press" },
                    { 23, 5, "arm circles" },
                    { 24, 5, "arnold dumbbell press" },
                    { 25, 6, "around the worlds" },
                    { 26, 11, "atlas stone trainer" },
                    { 27, 11, "atlas stones" },
                    { 28, 11, "axle deadlift" },
                    { 29, 5, "back flyes - with bands" },
                    { 30, 3, "backward drag" },
                    { 31, 5, "backward medicine ball throw" },
                    { 32, 9, "balance board" },
                    { 33, 7, "ball leg curl" },
                    { 34, 12, "band assisted pull-up" },
                    { 35, 7, "band good morning" },
                    { 36, 7, "band good morning (pull through)" },
                    { 37, 2, "band hip adductions" },
                    { 38, 5, "band pull apart" },
                    { 39, 13, "band skull crusher" },
                    { 40, 1, "barbell ab rollout" },
                    { 41, 1, "barbell ab rollout - on knees" },
                    { 42, 6, "barbell bench press - medium grip" },
                    { 43, 4, "barbell curl" },
                    { 44, 4, "barbell curls lying against an incline" },
                    { 45, 11, "barbell deadlift" },
                    { 46, 3, "barbell full squat" },
                    { 47, 10, "barbell glute bridge" },
                    { 48, 6, "barbell guillotine bench press" },
                    { 49, 3, "barbell hack squat" },
                    { 50, 10, "barbell hip thrust" },
                    { 51, 6, "barbell incline bench press - medium grip" },
                    { 52, 5, "barbell incline shoulder raise" },
                    { 53, 3, "barbell lunge" },
                    { 54, 5, "barbell rear delt row" },
                    { 55, 1, "barbell rollout from bench" },
                    { 56, 9, "barbell seated calf raise" },
                    { 57, 5, "barbell shoulder press" },
                    { 58, 14, "barbell shrug" },
                    { 59, 14, "barbell shrug behind the back" },
                    { 60, 1, "barbell side bend" },
                    { 61, 3, "barbell side split squat" },
                    { 62, 3, "barbell squat" },
                    { 63, 3, "barbell squat to a bench" },
                    { 64, 3, "barbell step ups" },
                    { 65, 3, "barbell walking lunge" },
                    { 66, 5, "battling ropes" },
                    { 67, 3, "bear crawl sled drags" },
                    { 68, 6, "behind head chest stretch" },
                    { 69, 13, "bench dips" },
                    { 70, 3, "bench jump" },
                    { 71, 13, "bench press - powerlifting" },
                    { 72, 6, "bench press - with bands" },
                    { 73, 13, "bench press with chains" },
                    { 74, 3, "bench sprint" },
                    { 75, 8, "bent over barbell row" },
                    { 76, 5, "bent over dumbbell rear delt raise with head on bench" },
                    { 77, 5, "bent over low-pulley side lateral" },
                    { 78, 8, "bent over one-arm long bar row" },
                    { 79, 8, "bent over two-arm long bar row" },
                    { 80, 8, "bent over two-dumbbell row" },
                    { 81, 8, "bent over two-dumbbell row with palms in" },
                    { 82, 1, "bent press" },
                    { 83, 12, "bent-arm barbell pullover" },
                    { 84, 6, "bent-arm dumbbell pullover" },
                    { 85, 1, "bent-knee hip raise" },
                    { 86, 3, "bicycling" },
                    { 87, 13, "board press" },
                    { 88, 13, "body tricep press" },
                    { 89, 13, "body-up" },
                    { 90, 6, "bodyweight flyes" },
                    { 91, 8, "bodyweight mid row" },
                    { 92, 3, "bodyweight squat" },
                    { 93, 3, "bodyweight walking lunge" },
                    { 94, 1, "bosu ball cable crunch with side bends" },
                    { 95, 1, "bottoms up" },
                    { 96, 15, "bottoms-up clean from the hang position" },
                    { 97, 7, "box jump (multiple response)" },
                    { 98, 7, "box skip" },
                    { 99, 3, "box squat" },
                    { 100, 3, "box squat with bands" },
                    { 101, 3, "box squat with chains" },
                    { 102, 4, "brachialis-smr" },
                    { 103, 5, "bradford/rocky presses" },
                    { 104, 10, "butt lift (bridge)" },
                    { 105, 1, "butt-ups" },
                    { 106, 6, "butterfly" },
                    { 107, 6, "cable chest press" },
                    { 108, 6, "cable crossover" },
                    { 109, 1, "cable crunch" },
                    { 110, 3, "cable deadlifts" },
                    { 111, 4, "cable hammer curls - rope attachment" },
                    { 112, 3, "cable hip adduction" },
                    { 113, 12, "cable incline pushdown" },
                    { 114, 13, "cable incline triceps extension" },
                    { 115, 5, "cable internal rotation" },
                    { 116, 6, "cable iron cross" },
                    { 117, 1, "cable judo flip" },
                    { 118, 13, "cable lying triceps extension" },
                    { 119, 13, "cable one arm tricep extension" },
                    { 120, 4, "cable preacher curl" },
                    { 121, 5, "cable rear delt fly" },
                    { 122, 1, "cable reverse crunch" },
                    { 123, 13, "cable rope overhead triceps extension" },
                    { 124, 5, "cable rope rear-delt rows" },
                    { 125, 1, "cable russian twists" },
                    { 126, 1, "cable seated crunch" },
                    { 127, 5, "cable seated lateral raise" },
                    { 128, 5, "cable shoulder press" },
                    { 129, 14, "cable shrugs" },
                    { 130, 1, "cable tuck reverse crunch" },
                    { 131, 15, "cable wrist curl" },
                    { 132, 9, "calf press" },
                    { 133, 9, "calf press on the leg press machine" },
                    { 134, 9, "calf raise on a dumbbell" },
                    { 135, 9, "calf raises - with bands" },
                    { 136, 9, "calf stretch elbows against wall" },
                    { 137, 9, "calf stretch hands against wall" },
                    { 138, 14, "calf-machine shoulder shrug" },
                    { 139, 9, "calves-smr" },
                    { 140, 3, "car deadlift" },
                    { 141, 5, "car drivers" },
                    { 142, 2, "carioca quick step" },
                    { 143, 11, "cat stretch" },
                    { 144, 12, "catch and overhead throw" },
                    { 145, 13, "chain handle extension" },
                    { 146, 6, "chain press" },
                    { 147, 7, "chair leg extended stretch" },
                    { 148, 12, "chair lower back stretch" },
                    { 149, 3, "chair squat" },
                    { 150, 5, "chair upper body stretch" },
                    { 151, 6, "chest and front of shoulder stretch" },
                    { 152, 6, "chest push (multiple response)" },
                    { 153, 6, "chest push (single response)" },
                    { 154, 6, "chest push from 3 point stance" },
                    { 155, 6, "chest push with run release" },
                    { 156, 6, "chest stretch on stability ball" },
                    { 157, 11, "child's pose" },
                    { 158, 16, "chin to chest stretch" },
                    { 159, 12, "chin-up" },
                    { 160, 5, "circus bell" },
                    { 161, 7, "clean" },
                    { 162, 5, "clean and jerk" },
                    { 163, 5, "clean and press" },
                    { 164, 7, "clean deadlift" },
                    { 165, 3, "clean from blocks" },
                    { 166, 3, "clean pull" },
                    { 167, 14, "clean shrug" },
                    { 168, 6, "clock push-up" },
                    { 169, 13, "close-grip barbell bench press" },
                    { 170, 13, "close-grip dumbbell press" },
                    { 171, 4, "close-grip ez bar curl" },
                    { 172, 4, "close-grip ez-bar curl with band" },
                    { 173, 13, "close-grip ez-bar press" },
                    { 174, 12, "close-grip front lat pulldown" },
                    { 175, 13, "close-grip push-up off of a dumbbell" },
                    { 176, 4, "close-grip standing barbell curl" },
                    { 177, 1, "cocoons" },
                    { 178, 3, "conan's wheel" },
                    { 179, 4, "concentration curls" },
                    { 180, 4, "cross body hammer curl" },
                    { 181, 6, "cross over - with bands" },
                    { 182, 1, "cross-body crunch" },
                    { 183, 11, "crossover reverse lunge" },
                    { 184, 5, "crucifix" },
                    { 185, 1, "crunch - hands overhead" },
                    { 186, 1, "crunch - legs on exercise ball" },
                    { 187, 1, "crunches" },
                    { 188, 5, "cuban press" },
                    { 189, 11, "dancer's stretch" },
                    { 190, 11, "deadlift with bands" },
                    { 191, 11, "deadlift with chains" },
                    { 192, 6, "decline barbell bench press" },
                    { 193, 13, "decline close-grip bench to skull crusher" },
                    { 194, 1, "decline crunch" },
                    { 195, 6, "decline dumbbell bench press" },
                    { 196, 6, "decline dumbbell flyes" },
                    { 197, 13, "decline dumbbell triceps extension" },
                    { 198, 13, "decline ez bar triceps extension" },
                    { 199, 1, "decline oblique crunch" },
                    { 200, 6, "decline push-up" },
                    { 201, 1, "decline reverse crunch" },
                    { 202, 6, "decline smith press" },
                    { 203, 11, "deficit deadlift" },
                    { 204, 3, "depth jump leap" },
                    { 205, 13, "dip machine" },
                    { 206, 6, "dips - chest version" },
                    { 207, 13, "dips - triceps version" },
                    { 208, 9, "donkey calf raises" },
                    { 209, 7, "double kettlebell alternating hang clean" },
                    { 210, 5, "double kettlebell jerk" },
                    { 211, 5, "double kettlebell push press" },
                    { 212, 5, "double kettlebell snatch" },
                    { 213, 1, "double kettlebell windmill" },
                    { 214, 3, "double leg butt kick" },
                    { 215, 10, "downward facing balance" },
                    { 216, 4, "drag curl" },
                    { 217, 6, "drop push" },
                    { 218, 4, "dumbbell alternate bicep curl" },
                    { 219, 6, "dumbbell bench press" },
                    { 220, 6, "dumbbell bench press with neutral grip" },
                    { 221, 4, "dumbbell bicep curl" },
                    { 222, 7, "dumbbell clean" },
                    { 223, 13, "dumbbell floor press" },
                    { 224, 6, "dumbbell flyes" },
                    { 225, 8, "dumbbell incline row" },
                    { 226, 5, "dumbbell incline shoulder raise" },
                    { 227, 3, "dumbbell lunges" },
                    { 228, 5, "dumbbell lying one-arm rear lateral raise" },
                    { 229, 15, "dumbbell lying pronation" },
                    { 230, 5, "dumbbell lying rear lateral raise" },
                    { 231, 15, "dumbbell lying supination" },
                    { 232, 5, "dumbbell one-arm shoulder press" },
                    { 233, 13, "dumbbell one-arm triceps extension" },
                    { 234, 5, "dumbbell one-arm upright row" },
                    { 235, 4, "dumbbell prone incline curl" },
                    { 236, 5, "dumbbell raise" },
                    { 237, 3, "dumbbell rear lunge" },
                    { 238, 5, "dumbbell scaption" },
                    { 239, 3, "dumbbell seated box jump" },
                    { 240, 9, "dumbbell seated one-leg calf raise" },
                    { 241, 5, "dumbbell shoulder press" },
                    { 242, 14, "dumbbell shrug" },
                    { 243, 1, "dumbbell side bend" },
                    { 244, 3, "dumbbell squat" },
                    { 245, 3, "dumbbell squat to a bench" },
                    { 246, 3, "dumbbell step ups" },
                    { 247, 13, "dumbbell tricep extension -pronated grip" },
                    { 248, 12, "dynamic back stretch" },
                    { 249, 6, "dynamic chest stretch" },
                    { 250, 5, "elbow circles" },
                    { 251, 1, "elbow to knee" },
                    { 252, 6, "elbows back" },
                    { 253, 3, "elevated back lunge" },
                    { 254, 12, "elevated cable rows" },
                    { 255, 3, "elliptical trainer" },
                    { 256, 1, "exercise ball crunch" },
                    { 257, 1, "exercise ball pull-in" },
                    { 258, 6, "extended range one-arm kettlebell floor press" },
                    { 259, 5, "external rotation" },
                    { 260, 5, "external rotation with band" },
                    { 261, 5, "external rotation with cable" },
                    { 262, 4, "ez-bar curl" },
                    { 263, 13, "ez-bar skullcrusher" },
                    { 264, 5, "face pull" },
                    { 265, 15, "farmer's walk" },
                    { 266, 3, "fast skipping" },
                    { 267, 15, "finger curls" },
                    { 268, 6, "flat bench cable flyes" },
                    { 269, 1, "flat bench leg pull-in" },
                    { 270, 1, "flat bench lying leg raise" },
                    { 271, 4, "flexor incline dumbbell curls" },
                    { 272, 7, "floor glute-ham raise" },
                    { 273, 13, "floor press" },
                    { 274, 13, "floor press with chains" },
                    { 275, 10, "flutter kicks" },
                    { 276, 9, "foot-smr" },
                    { 277, 6, "forward drag with press" },
                    { 278, 3, "frankenstein squat" },
                    { 279, 3, "freehand jump squat" },
                    { 280, 3, "frog hops" },
                    { 281, 1, "frog sit-ups" },
                    { 282, 3, "front barbell squat" },
                    { 283, 3, "front barbell squat to a bench" },
                    { 284, 7, "front box jump" },
                    { 285, 5, "front cable raise" },
                    { 286, 3, "front cone hops (or hurdle hops)" },
                    { 287, 5, "front dumbbell raise" },
                    { 288, 5, "front incline dumbbell raise" },
                    { 289, 7, "front leg raises" },
                    { 290, 5, "front plate raise" },
                    { 291, 6, "front raise and pullover" },
                    { 292, 3, "front squat (clean grip)" },
                    { 293, 3, "front squats with two kettlebells" },
                    { 294, 5, "front two-dumbbell raise" },
                    { 295, 12, "full range-of-motion lat pulldown" },
                    { 296, 12, "gironda sternum chins" },
                    { 297, 7, "glute ham raise" },
                    { 298, 10, "glute kickback" },
                    { 299, 3, "goblet squat" },
                    { 300, 7, "good morning" },
                    { 301, 7, "good morning off pins" },
                    { 302, 1, "gorilla chin/crunch" },
                    { 303, 2, "groin and back stretch" },
                    { 304, 2, "groiners" },
                    { 305, 3, "hack squat" },
                    { 306, 4, "hammer curls" },
                    { 307, 6, "hammer grip incline db bench press" },
                    { 308, 7, "hamstring stretch" },
                    { 309, 7, "hamstring-smr" },
                    { 310, 5, "handstand push-ups" },
                    { 311, 3, "hang clean" },
                    { 312, 3, "hang clean - below the knees" },
                    { 313, 7, "hang snatch" },
                    { 314, 7, "hang snatch - below knees" },
                    { 315, 7, "hanging bar good morning" },
                    { 316, 1, "hanging leg raise" },
                    { 317, 1, "hanging pike" },
                    { 318, 3, "heaving snatch balance" },
                    { 319, 6, "heavy bag thrust" },
                    { 320, 4, "high cable curls" },
                    { 321, 17, "hip circles (prone)" },
                    { 322, 18, "hip crossover" },
                    { 323, 10, "hip extension with bands" },
                    { 324, 3, "hip flexion with band" },
                    { 325, 10, "hip lift with band" },
                    { 326, 11, "hug a ball" },
                    { 327, 11, "hug knees to chest" },
                    { 328, 7, "hurdle hops" },
                    { 329, 11, "hyperextensions (back extensions)" },
                    { 330, 11, "hyperextensions with no hyperextension bench" },
                    { 331, 17, "iliotibial tract-smr" },
                    { 332, 13, "incline barbell triceps extension" },
                    { 333, 8, "incline bench pull" },
                    { 334, 6, "incline cable chest press" },
                    { 335, 6, "incline cable flye" },
                    { 336, 6, "incline dumbbell bench with palms facing in" },
                    { 337, 4, "incline dumbbell curl" },
                    { 338, 6, "incline dumbbell flyes" },
                    { 339, 6, "incline dumbbell flyes - with a twist" },
                    { 340, 6, "incline dumbbell press" },
                    { 341, 4, "incline hammer curls" },
                    { 342, 4, "incline inner biceps curl" },
                    { 343, 6, "incline push-up" },
                    { 344, 13, "incline push-up close-grip" },
                    { 345, 6, "incline push-up depth jump" },
                    { 346, 6, "incline push-up medium" },
                    { 347, 6, "incline push-up reverse grip" },
                    { 348, 6, "incline push-up wide" },
                    { 349, 7, "intermediate groin stretch" },
                    { 350, 3, "intermediate hip flexor and quad stretch" },
                    { 351, 5, "internal rotation with band" },
                    { 352, 8, "inverted row" },
                    { 353, 8, "inverted row with straps" },
                    { 354, 5, "iron cross" },
                    { 355, 3, "iron crosses (stretch)" },
                    { 356, 6, "isometric chest squeezes" },
                    { 357, 16, "isometric neck exercise - front and back" },
                    { 358, 16, "isometric neck exercise - sides" },
                    { 359, 6, "isometric wipers" },
                    { 360, 17, "it band and glute stretch" },
                    { 361, 1, "jackknife sit-up" },
                    { 362, 1, "janda sit-up" },
                    { 363, 3, "jefferson squats" },
                    { 364, 5, "jerk balance" },
                    { 365, 3, "jerk dip squat" },
                    { 366, 13, "jm press" },
                    { 367, 3, "jogging-treadmill" },
                    { 368, 11, "keg load" },
                    { 369, 5, "kettlebell arnold press" },
                    { 370, 7, "kettlebell dead clean" },
                    { 371, 1, "kettlebell figure 8" },
                    { 372, 7, "kettlebell hang clean" },
                    { 373, 7, "kettlebell one-legged deadlift" },
                    { 374, 1, "kettlebell pass between the legs" },
                    { 375, 5, "kettlebell pirate ships" },
                    { 376, 3, "kettlebell pistol squat" },
                    { 377, 5, "kettlebell seated press" },
                    { 378, 5, "kettlebell seesaw press" },
                    { 379, 14, "kettlebell sumo high pull" },
                    { 380, 5, "kettlebell thruster" },
                    { 381, 5, "kettlebell turkish get-up (lunge style)" },
                    { 382, 5, "kettlebell turkish get-up (squat style)" },
                    { 383, 1, "kettlebell windmill" },
                    { 384, 12, "kipping muscle up" },
                    { 385, 10, "knee across the body" },
                    { 386, 9, "knee circles" },
                    { 387, 7, "knee tuck jump" },
                    { 388, 1, "knee/hip raise on parallel bars" },
                    { 389, 5, "kneeling arm drill" },
                    { 390, 1, "kneeling cable crunch with alternating oblique twists" },
                    { 391, 13, "kneeling cable triceps extension" },
                    { 392, 15, "kneeling forearm stretch" },
                    { 393, 12, "kneeling high pulley row" },
                    { 394, 3, "kneeling hip flexor" },
                    { 395, 10, "kneeling jump squat" },
                    { 396, 12, "kneeling single-arm high pulley row" },
                    { 397, 10, "kneeling squat" },
                    { 398, 1, "landmine 180's" },
                    { 399, 5, "landmine linear jammer" },
                    { 400, 2, "lateral bound" },
                    { 401, 2, "lateral box jump" },
                    { 402, 2, "lateral cone hops" },
                    { 403, 5, "lateral raise - with bands" },
                    { 404, 12, "latissimus dorsi-smr" },
                    { 405, 3, "leg extensions" },
                    { 406, 10, "leg lift" },
                    { 407, 3, "leg press" },
                    { 408, 1, "leg pull-in" },
                    { 409, 6, "leg-over floor press" },
                    { 410, 7, "leg-up hamstring stretch" },
                    { 411, 6, "leverage chest press" },
                    { 412, 3, "leverage deadlift" },
                    { 413, 6, "leverage decline chest press" },
                    { 414, 8, "leverage high row" },
                    { 415, 6, "leverage incline chest press" },
                    { 416, 12, "leverage iso row" },
                    { 417, 5, "leverage shoulder press" },
                    { 418, 14, "leverage shrug" },
                    { 419, 7, "linear 3-part start technique" },
                    { 420, 7, "linear acceleration wall drill" },
                    { 421, 3, "linear depth jump" },
                    { 422, 5, "log lift" },
                    { 423, 12, "london bridges" },
                    { 424, 3, "looking at ceiling" },
                    { 425, 6, "low cable crossover" },
                    { 426, 13, "low cable triceps extension" },
                    { 427, 5, "low pulley row to neck" },
                    { 428, 1, "lower back curl" },
                    { 429, 11, "lower back-smr" },
                    { 430, 7, "lunge pass through" },
                    { 431, 3, "lunge sprint" },
                    { 432, 2, "lying bent leg groin" },
                    { 433, 4, "lying cable curl" },
                    { 434, 8, "lying cambered barbell row" },
                    { 435, 4, "lying close-grip bar curl on high pulley" },
                    { 436, 13, "lying close-grip barbell triceps extension behind the head" },
                    { 437, 13, "lying close-grip barbell triceps press to chin" },
                    { 438, 17, "lying crossover" },
                    { 439, 13, "lying dumbbell tricep extension" },
                    { 440, 16, "lying face down plate neck resistance" },
                    { 441, 16, "lying face up plate neck resistance" },
                    { 442, 10, "lying glute" },
                    { 443, 7, "lying hamstring" },
                    { 444, 4, "lying high bench barbell curl" },
                    { 445, 7, "lying leg curls" },
                    { 446, 3, "lying machine squat" },
                    { 447, 5, "lying one-arm lateral raise" },
                    { 448, 3, "lying prone quadriceps" },
                    { 449, 5, "lying rear delt raise" },
                    { 450, 4, "lying supine dumbbell curl" },
                    { 451, 8, "lying t-bar row" },
                    { 452, 13, "lying triceps press" },
                    { 453, 6, "machine bench press" },
                    { 454, 4, "machine bicep curl" },
                    { 455, 4, "machine preacher curls" },
                    { 456, 5, "machine shoulder (military) press" },
                    { 457, 13, "machine triceps extension" },
                    { 458, 6, "medicine ball chest pass" },
                    { 459, 1, "medicine ball full twist" },
                    { 460, 5, "medicine ball scoop throw" },
                    { 461, 8, "middle back shrug" },
                    { 462, 8, "middle back stretch" },
                    { 463, 8, "mixed grip chin" },
                    { 464, 17, "monster walk" },
                    { 465, 3, "mountain climbers" },
                    { 466, 7, "moving claw series" },
                    { 467, 7, "muscle snatch" },
                    { 468, 12, "muscle up" },
                    { 469, 3, "narrow stance hack squats" },
                    { 470, 3, "narrow stance leg press" },
                    { 471, 3, "narrow stance squats" },
                    { 472, 7, "natural glute ham raise" },
                    { 473, 6, "neck press" },
                    { 474, 16, "neck-smr" },
                    { 475, 1, "oblique crunches" },
                    { 476, 1, "oblique crunches - on the floor" },
                    { 477, 3, "olympic squat" },
                    { 478, 3, "on your side quad stretch" },
                    { 479, 3, "on-your-back quad stretch" },
                    { 480, 12, "one arm against wall" },
                    { 481, 8, "one arm chin-up" },
                    { 482, 6, "one arm dumbbell bench press" },
                    { 483, 4, "one arm dumbbell preacher curl" },
                    { 484, 13, "one arm floor press" },
                    { 485, 12, "one arm lat pulldown" },
                    { 486, 13, "one arm pronated dumbbell triceps extension" },
                    { 487, 13, "one arm supinated dumbbell triceps extension" },
                    { 488, 3, "one half locust" },
                    { 489, 12, "one handed hang" },
                    { 490, 10, "one knee to chest" },
                    { 491, 3, "one leg barbell squat" },
                    { 492, 8, "one-arm dumbbell row" },
                    { 493, 6, "one-arm flat bench dumbbell flye" },
                    { 494, 1, "one-arm high-pulley cable side bends" },
                    { 495, 5, "one-arm incline lateral raise" },
                    { 496, 7, "one-arm kettlebell clean" },
                    { 497, 5, "one-arm kettlebell clean and jerk" },
                    { 498, 6, "one-arm kettlebell floor press" },
                    { 499, 5, "one-arm kettlebell jerk" },
                    { 500, 5, "one-arm kettlebell military press to the side" },
                    { 501, 5, "one-arm kettlebell para press" },
                    { 502, 5, "one-arm kettlebell push press" },
                    { 503, 8, "one-arm kettlebell row" },
                    { 504, 5, "one-arm kettlebell snatch" },
                    { 505, 5, "one-arm kettlebell split jerk" },
                    { 506, 5, "one-arm kettlebell split snatch" },
                    { 507, 7, "one-arm kettlebell swings" },
                    { 508, 8, "one-arm long bar row" },
                    { 509, 1, "one-arm medicine ball slam" },
                    { 510, 7, "one-arm open palm kettlebell clean" },
                    { 511, 3, "one-arm overhead kettlebell squats" },
                    { 512, 3, "one-arm side deadlift" },
                    { 513, 5, "one-arm side laterals" },
                    { 514, 10, "one-legged cable kickback" },
                    { 515, 7, "open palm kettlebell clean" },
                    { 516, 1, "otis-up" },
                    { 517, 4, "overhead cable curl" },
                    { 518, 12, "overhead lat" },
                    { 519, 12, "overhead slam" },
                    { 520, 3, "overhead squat" },
                    { 521, 1, "overhead stretch" },
                    { 522, 13, "overhead triceps" },
                    { 523, 1, "pallof press with rotation" },
                    { 524, 15, "palms-down dumbbell wrist curl over a bench" },
                    { 525, 15, "palms-down wrist curl over a bench" },
                    { 526, 15, "palms-up barbell wrist curl over a bench" },
                    { 527, 15, "palms-up dumbbell wrist curl over a bench" },
                    { 528, 13, "parallel bar dip" },
                    { 529, 11, "pelvic tilt into bridge" },
                    { 530, 9, "peroneals stretch" },
                    { 531, 9, "peroneals-smr" },
                    { 532, 10, "physioball hip bridge" },
                    { 533, 13, "pin presses" },
                    { 534, 10, "piriformis-smr" },
                    { 535, 1, "plank" },
                    { 536, 18, "plank with twist" },
                    { 537, 15, "plate pinch" },
                    { 538, 1, "plate twist" },
                    { 539, 7, "platform hamstring slides" },
                    { 540, 3, "plie dumbbell squat" },
                    { 541, 6, "plyo kettlebell pushups" },
                    { 542, 6, "plyo push-up" },
                    { 543, 9, "posterior tibialis stretch" },
                    { 544, 7, "power clean" },
                    { 545, 7, "power clean from blocks" },
                    { 546, 3, "power jerk" },
                    { 547, 5, "power partials" },
                    { 548, 7, "power snatch" },
                    { 549, 3, "power snatch from blocks" },
                    { 550, 7, "power stairs" },
                    { 551, 4, "preacher curl" },
                    { 552, 4, "preacher hammer dumbbell curl" },
                    { 553, 1, "press sit-up" },
                    { 554, 7, "prone manual hamstring" },
                    { 555, 7, "prowler sprint" },
                    { 556, 10, "pull through" },
                    { 557, 12, "pullups" },
                    { 558, 5, "push press" },
                    { 559, 5, "push press - behind the neck" },
                    { 560, 6, "push up to side plank" },
                    { 561, 6, "push-up wide" },
                    { 562, 13, "push-ups - close triceps position" },
                    { 563, 6, "push-ups with feet elevated" },
                    { 564, 6, "push-ups with feet on an exercise ball" },
                    { 565, 6, "pushups" },
                    { 566, 6, "pushups (close and wide hand positions)" },
                    { 567, 11, "pyramid" },
                    { 568, 3, "quad stretch" },
                    { 569, 3, "quadriceps-smr" },
                    { 570, 3, "quick leap" },
                    { 571, 5, "rack delivery" },
                    { 572, 11, "rack pull with bands" },
                    { 573, 11, "rack pulls" },
                    { 574, 3, "rear leg raises" },
                    { 575, 3, "recumbent bike" },
                    { 576, 5, "return push from stance" },
                    { 577, 13, "reverse band bench press" },
                    { 578, 3, "reverse band box squat" },
                    { 579, 11, "reverse band deadlift" },
                    { 580, 3, "reverse band power squat" },
                    { 581, 7, "reverse band sumo deadlift" },
                    { 582, 4, "reverse barbell curl" },
                    { 583, 4, "reverse barbell preacher curls" },
                    { 584, 4, "reverse cable curl" },
                    { 585, 1, "reverse crunch" },
                    { 586, 5, "reverse flyes" },
                    { 587, 5, "reverse flyes with external rotation" },
                    { 588, 8, "reverse grip bent-over rows" },
                    { 589, 13, "reverse grip triceps pushdown" },
                    { 590, 7, "reverse hyperextension" },
                    { 591, 5, "reverse machine flyes" },
                    { 592, 4, "reverse plate curls" },
                    { 593, 13, "reverse triceps bench press" },
                    { 594, 8, "rhomboids-smr" },
                    { 595, 15, "rickshaw carry" },
                    { 596, 3, "rickshaw deadlift" },
                    { 597, 13, "ring dips" },
                    { 598, 3, "rocket jump" },
                    { 599, 9, "rocking standing calf raise" },
                    { 600, 12, "rocky pull-ups/pulldowns" },
                    { 601, 7, "romanian deadlift" },
                    { 602, 7, "romanian deadlift from deficit" },
                    { 603, 12, "rope climb" },
                    { 604, 1, "rope crunch" },
                    { 605, 3, "rope jumping" },
                    { 606, 12, "rope straight-arm pulldown" },
                    { 607, 5, "round the world shoulder stretch" },
                    { 608, 7, "runner's stretch" },
                    { 609, 1, "russian twist" },
                    { 610, 3, "sandbag load" },
                    { 611, 14, "scapular pull-up" },
                    { 612, 1, "scissor kick" },
                    { 613, 3, "scissors jump" },
                    { 614, 7, "seated band hamstring curl" },
                    { 615, 5, "seated barbell military press" },
                    { 616, 1, "seated barbell twist" },
                    { 617, 13, "seated bent-over one-arm dumbbell triceps extension" },
                    { 618, 5, "seated bent-over rear delt raise" },
                    { 619, 13, "seated bent-over two-arm dumbbell triceps extension" },
                    { 620, 4, "seated biceps" },
                    { 621, 8, "seated cable rows" },
                    { 622, 5, "seated cable shoulder press" },
                    { 623, 9, "seated calf raise" },
                    { 624, 9, "seated calf stretch" },
                    { 625, 4, "seated close-grip concentration barbell curl" },
                    { 626, 4, "seated dumbbell curl" },
                    { 627, 4, "seated dumbbell inner biceps curl" },
                    { 628, 15, "seated dumbbell palms-down wrist curl" },
                    { 629, 15, "seated dumbbell palms-up wrist curl" },
                    { 630, 5, "seated dumbbell press" },
                    { 631, 1, "seated flat bench leg pull-in" },
                    { 632, 7, "seated floor hamstring stretch" },
                    { 633, 5, "seated front deltoid" },
                    { 634, 10, "seated glute" },
                    { 635, 11, "seated good mornings" },
                    { 636, 7, "seated hamstring" },
                    { 637, 7, "seated hamstring and calf stretch" },
                    { 638, 16, "seated head harness neck resistance" },
                    { 639, 7, "seated leg curl" },
                    { 640, 1, "seated leg tucks" },
                    { 641, 8, "seated one-arm cable pulley rows" },
                    { 642, 15, "seated one-arm dumbbell palms-down wrist curl" },
                    { 643, 15, "seated one-arm dumbbell palms-up wrist curl" },
                    { 644, 1, "seated overhead stretch" },
                    { 645, 15, "seated palm-up barbell wrist curl" },
                    { 646, 15, "seated palms-down barbell wrist curl" },
                    { 647, 5, "seated side lateral raise" },
                    { 648, 13, "seated triceps press" },
                    { 649, 15, "seated two-arm palms-up low-pulley wrist curl" },
                    { 650, 5, "see-saw press (alternating side press)" },
                    { 651, 12, "shotgun row" },
                    { 652, 5, "shoulder circles" },
                    { 653, 5, "shoulder press - with bands" },
                    { 654, 5, "shoulder raise" },
                    { 655, 5, "shoulder stretch" },
                    { 656, 1, "side bridge" },
                    { 657, 3, "side hop-sprint" },
                    { 658, 1, "side jackknife" },
                    { 659, 5, "side lateral raise" },
                    { 660, 5, "side laterals to front raise" },
                    { 661, 2, "side leg raises" },
                    { 662, 2, "side lying groin stretch" },
                    { 663, 16, "side neck stretch" },
                    { 664, 3, "side standing long jump" },
                    { 665, 3, "side to side box shuffle" },
                    { 666, 12, "side to side chins" },
                    { 667, 5, "side wrist pull" },
                    { 668, 12, "side-lying floor stretch" },
                    { 669, 5, "single dumbbell raise" },
                    { 670, 3, "single leg butt kick" },
                    { 671, 10, "single leg glute bridge" },
                    { 672, 3, "single leg push-off" },
                    { 673, 6, "single-arm cable crossover" },
                    { 674, 5, "single-arm linear jammer" },
                    { 675, 6, "single-arm push-up" },
                    { 676, 3, "single-cone sprint drill" },
                    { 677, 3, "single-leg high box squat" },
                    { 678, 3, "single-leg hop progression" },
                    { 679, 3, "single-leg lateral hop" },
                    { 680, 3, "single-leg leg extension" },
                    { 681, 3, "single-leg stride jump" },
                    { 682, 3, "sit squats" },
                    { 683, 1, "sit-up" },
                    { 684, 3, "skating" },
                    { 685, 3, "sled drag - harness" },
                    { 686, 5, "sled overhead backward walk" },
                    { 687, 13, "sled overhead triceps extension" },
                    { 688, 3, "sled push" },
                    { 689, 5, "sled reverse flye" },
                    { 690, 8, "sled row" },
                    { 691, 1, "sledgehammer swings" },
                    { 692, 5, "smith incline shoulder raise" },
                    { 693, 14, "smith machine behind the back shrug" },
                    { 694, 6, "smith machine bench press" },
                    { 695, 8, "smith machine bent over row" },
                    { 696, 9, "smith machine calf raise" },
                    { 697, 13, "smith machine close-grip bench press" },
                    { 698, 6, "smith machine decline press" },
                    { 699, 7, "smith machine hang power clean" },
                    { 700, 1, "smith machine hip raise" },
                    { 701, 6, "smith machine incline bench press" },
                    { 702, 3, "smith machine leg press" },
                    { 703, 5, "smith machine one-arm upright row" },
                    { 704, 5, "smith machine overhead shoulder press" },
                    { 705, 3, "smith machine pistol squat" },
                    { 706, 9, "smith machine reverse calf raises" },
                    { 707, 14, "smith machine shrug" },
                    { 708, 3, "smith machine squat" },
                    { 709, 7, "smith machine stiff-legged deadlift" },
                    { 710, 14, "smith machine upright row" },
                    { 711, 3, "smith single-leg split squat" },
                    { 712, 3, "snatch" },
                    { 713, 3, "snatch balance" },
                    { 714, 7, "snatch deadlift" },
                    { 715, 3, "snatch from blocks" },
                    { 716, 7, "snatch pull" },
                    { 717, 14, "snatch shrug" },
                    { 718, 13, "speed band overhead triceps" },
                    { 719, 13, "speed band pushdown" },
                    { 720, 3, "speed box squat" },
                    { 721, 3, "speed squats" },
                    { 722, 1, "spell caster" },
                    { 723, 1, "spider crawl" },
                    { 724, 4, "spider curl" },
                    { 725, 8, "spinal stretch" },
                    { 726, 3, "split clean" },
                    { 727, 3, "split jerk" },
                    { 728, 3, "split jump" },
                    { 729, 7, "split snatch" },
                    { 730, 3, "split squat with dumbbells" },
                    { 731, 7, "split squats" },
                    { 732, 3, "squat jerk" },
                    { 733, 3, "squat with bands" },
                    { 734, 3, "squat with chains" },
                    { 735, 3, "squat with plate movers" },
                    { 736, 3, "squats - with bands" },
                    { 737, 3, "stairmaster" },
                    { 738, 5, "standing alternating dumbbell press" },
                    { 739, 9, "standing barbell calf raise" },
                    { 740, 5, "standing barbell press behind neck" },
                    { 741, 13, "standing bent-over one-arm dumbbell triceps extension" },
                    { 742, 13, "standing bent-over two-arm dumbbell triceps extension" },
                    { 743, 4, "standing biceps cable curl" },
                    { 744, 4, "standing biceps stretch" },
                    { 745, 5, "standing bradford press" },
                    { 746, 6, "standing cable chest press" },
                    { 747, 1, "standing cable lift" },
                    { 748, 1, "standing cable wood chop" },
                    { 749, 9, "standing calf raises" },
                    { 750, 4, "standing concentration curl" },
                    { 751, 9, "standing dumbbell calf raise" },
                    { 752, 5, "standing dumbbell press" },
                    { 753, 4, "standing dumbbell reverse curl" },
                    { 754, 5, "standing dumbbell straight-arm front delt raise above head" },
                    { 755, 13, "standing dumbbell triceps extension" },
                    { 756, 14, "standing dumbbell upright row" },
                    { 757, 3, "standing elevated quad stretch" },
                    { 758, 5, "standing front barbell raise over head" },
                    { 759, 9, "standing gastrocnemius calf stretch" },
                    { 760, 7, "standing hamstring and calf stretch" },
                    { 761, 17, "standing hip circles" },
                    { 762, 3, "standing hip flexors" },
                    { 763, 4, "standing inner-biceps curl" },
                    { 764, 1, "standing lateral stretch" },
                    { 765, 7, "standing leg curl" },
                    { 766, 3, "standing long jump" },
                    { 767, 5, "standing low-pulley deltoid raise" },
                    { 768, 13, "standing low-pulley one-arm triceps extension" },
                    { 769, 5, "standing military press" },
                    { 770, 15, "standing olympic plate hand squeeze" },
                    { 771, 4, "standing one-arm cable curl" },
                    { 772, 4, "standing one-arm dumbbell curl over incline bench" },
                    { 773, 13, "standing one-arm dumbbell triceps extension" },
                    { 774, 13, "standing overhead barbell triceps extension" },
                    { 775, 5, "standing palm-in one-arm dumbbell press" },
                    { 776, 5, "standing palms-in dumbbell press" },
                    { 777, 15, "standing palms-up barbell behind the back wrist curl" },
                    { 778, 11, "standing pelvic tilt" },
                    { 779, 1, "standing rope crunch" },
                    { 780, 9, "standing soleus and achilles stretch" },
                    { 781, 7, "standing toe touches" },
                    { 782, 13, "standing towel triceps extension" },
                    { 783, 5, "standing two-arm overhead throw" },
                    { 784, 3, "star jump" },
                    { 785, 3, "step mill" },
                    { 786, 10, "step-up with knee raise" },
                    { 787, 11, "stiff leg barbell good morning" },
                    { 788, 7, "stiff-legged barbell deadlift" },
                    { 789, 7, "stiff-legged dumbbell deadlift" },
                    { 790, 1, "stomach vacuum" },
                    { 791, 8, "straight bar bench mid rows" },
                    { 792, 5, "straight raises on incline bench" },
                    { 793, 6, "straight-arm dumbbell pullover" },
                    { 794, 12, "straight-arm pulldown" },
                    { 795, 3, "stride jump crossover" },
                    { 796, 7, "sumo deadlift" },
                    { 797, 7, "sumo deadlift with bands" },
                    { 798, 7, "sumo deadlift with chains" },
                    { 799, 11, "superman" },
                    { 800, 13, "supine chest throw" },
                    { 801, 1, "supine one-arm overhead throw" },
                    { 802, 1, "supine two-arm overhead throw" },
                    { 803, 6, "suspended push-up" },
                    { 804, 1, "suspended reverse crunch" },
                    { 805, 8, "suspended row" },
                    { 806, 3, "suspended split squat" },
                    { 807, 8, "t-bar row with handle" },
                    { 808, 13, "tate press" },
                    { 809, 7, "the straddle" },
                    { 810, 17, "thigh abductor" },
                    { 811, 2, "thigh adductor" },
                    { 812, 3, "tire flip" },
                    { 813, 1, "toe touchers" },
                    { 814, 1, "torso rotation" },
                    { 815, 3, "trail running/walking" },
                    { 816, 3, "trap bar deadlift" },
                    { 817, 13, "tricep dumbbell kickback" },
                    { 818, 13, "tricep side stretch" },
                    { 819, 13, "triceps overhead extension with rope" },
                    { 820, 13, "triceps pushdown" },
                    { 821, 13, "triceps pushdown - rope attachment" },
                    { 822, 13, "triceps pushdown - v-bar attachment" },
                    { 823, 13, "triceps stretch" },
                    { 824, 1, "tuck crunch" },
                    { 825, 4, "two-arm dumbbell preacher curl" },
                    { 826, 5, "two-arm kettlebell clean" },
                    { 827, 5, "two-arm kettlebell jerk" },
                    { 828, 5, "two-arm kettlebell military press" },
                    { 829, 8, "two-arm kettlebell row" },
                    { 830, 12, "underhand cable pulldowns" },
                    { 831, 8, "upper back stretch" },
                    { 832, 7, "upper back-leg grab" },
                    { 833, 5, "upright barbell row" },
                    { 834, 14, "upright cable row" },
                    { 835, 14, "upright row - with bands" },
                    { 836, 5, "upward stretch" },
                    { 837, 12, "v-bar pulldown" },
                    { 838, 12, "v-bar pullup" },
                    { 839, 7, "vertical swing" },
                    { 840, 11, "weighted ball hyperextension" },
                    { 841, 1, "weighted ball side bend" },
                    { 842, 13, "weighted bench dip" },
                    { 843, 1, "weighted crunches" },
                    { 844, 3, "weighted jump squat" },
                    { 845, 12, "weighted pull ups" },
                    { 846, 3, "weighted sissy squat" },
                    { 847, 1, "weighted sit-ups - with bands" },
                    { 848, 3, "weighted squat" },
                    { 849, 3, "wide stance barbell squat" },
                    { 850, 7, "wide stance stiff legs" },
                    { 851, 6, "wide-grip barbell bench press" },
                    { 852, 6, "wide-grip decline barbell bench press" },
                    { 853, 6, "wide-grip decline barbell pullover" },
                    { 854, 12, "wide-grip lat pulldown" },
                    { 855, 12, "wide-grip pulldown behind the neck" },
                    { 856, 12, "wide-grip rear pull-up" },
                    { 857, 4, "wide-grip standing barbell curl" },
                    { 858, 1, "wind sprints" },
                    { 859, 17, "windmills" },
                    { 860, 7, "world's greatest stretch" },
                    { 861, 15, "wrist circles" },
                    { 862, 15, "wrist roller" },
                    { 863, 15, "wrist rotations with straight bar" },
                    { 864, 3, "yoke walk" },
                    { 865, 3, "zercher squats" },
                    { 866, 4, "zottman curl" },
                    { 867, 4, "zottman preacher curl" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_MuscleGroupId",
                table: "Exercises",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_CustomerId",
                table: "ProgressLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_ExerciseId",
                table: "ProgressLogs",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCustomer_TrainerId",
                table: "TrainerCustomer",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRequests_CustomerId",
                table: "TrainerRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRequests_TrainerId",
                table: "TrainerRequests",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanEntries_ExerciseId",
                table: "WorkoutPlanEntries",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanEntries_WorkoutPlanId",
                table: "WorkoutPlanEntries",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_AssignedToId",
                table: "WorkoutPlans",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_CreatedById",
                table: "WorkoutPlans",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_TrainerId",
                table: "WorkoutPlans",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProgressLogs");

            migrationBuilder.DropTable(
                name: "TrainerCustomer");

            migrationBuilder.DropTable(
                name: "TrainerRequests");

            migrationBuilder.DropTable(
                name: "WorkoutPlanEntries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
