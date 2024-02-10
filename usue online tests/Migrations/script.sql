CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

ALTER TABLE "UserExamResults" ALTER COLUMN "DateTimeStart" TYPE timestamp with time zone;

ALTER TABLE "ExamTestAnswers" ALTER COLUMN "DateTimeStart" TYPE timestamp with time zone;

ALTER TABLE "ExamTestAnswers" ALTER COLUMN "DateTimeEnd" TYPE timestamp with time zone;

ALTER TABLE "Exams" ALTER COLUMN "DateTimeStart" TYPE timestamp with time zone;

ALTER TABLE "Exams" ALTER COLUMN "DateTimeEnd" TYPE timestamp with time zone;

CREATE TABLE "PredictionResults" (
    "Id" uuid NOT NULL,
    "HeadIndex" integer NOT NULL,
    "HeadName" text NULL,
    CONSTRAINT "PK_PredictionResults" PRIMARY KEY ("Id")
);

CREATE TABLE "PredictionCategories" (
    "Id" uuid NOT NULL,
    "CategoryName" text NULL,
    "DisplayName" text NULL,
    "Index" integer NOT NULL,
    "Score" double precision NOT NULL,
    "PredictionResultId" uuid NOT NULL,
    CONSTRAINT "PK_PredictionCategories" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PredictionCategories_PredictionResults_PredictionResultId" FOREIGN KEY ("PredictionResultId") REFERENCES "PredictionResults" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PredictionCategories_PredictionResultId" ON "PredictionCategories" ("PredictionResultId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230923203550_ai', '6.0.22');

COMMIT;

START TRANSACTION;

ALTER TABLE "PredictionResults" ADD "Created" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230924071431_created_time', '6.0.22');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240210151804_exam results collection', '6.0.22');

COMMIT;

START TRANSACTION;

ALTER TABLE "Exams" DROP CONSTRAINT "FK_Exams_Presets_PresetId";

ALTER TABLE "Presets" DROP CONSTRAINT "FK_Presets_Users_OwnerId";

UPDATE "Presets" SET "OwnerId" = 0 WHERE "OwnerId" IS NULL;
ALTER TABLE "Presets" ALTER COLUMN "OwnerId" SET NOT NULL;
ALTER TABLE "Presets" ALTER COLUMN "OwnerId" SET DEFAULT 0;

UPDATE "Exams" SET "PresetId" = 0 WHERE "PresetId" IS NULL;
ALTER TABLE "Exams" ALTER COLUMN "PresetId" SET NOT NULL;
ALTER TABLE "Exams" ALTER COLUMN "PresetId" SET DEFAULT 0;

ALTER TABLE "Exams" ADD CONSTRAINT "FK_Exams_Presets_PresetId" FOREIGN KEY ("PresetId") REFERENCES "Presets" ("Id") ON DELETE CASCADE;

ALTER TABLE "Presets" ADD CONSTRAINT "FK_Presets_Users_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES "Users" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240210160605_update foreign key', '6.0.22');

COMMIT;

START TRANSACTION;

ALTER TABLE "UserExamResults" DROP CONSTRAINT "FK_UserExamResults_Exams_ExamId";

ALTER TABLE "UserExamResults" DROP CONSTRAINT "FK_UserExamResults_Users_UserId";

UPDATE "UserExamResults" SET "UserId" = 0 WHERE "UserId" IS NULL;
ALTER TABLE "UserExamResults" ALTER COLUMN "UserId" SET NOT NULL;
ALTER TABLE "UserExamResults" ALTER COLUMN "UserId" SET DEFAULT 0;

UPDATE "UserExamResults" SET "ExamId" = 0 WHERE "ExamId" IS NULL;
ALTER TABLE "UserExamResults" ALTER COLUMN "ExamId" SET NOT NULL;
ALTER TABLE "UserExamResults" ALTER COLUMN "ExamId" SET DEFAULT 0;

ALTER TABLE "UserExamResults" ADD CONSTRAINT "FK_UserExamResults_Exams_ExamId" FOREIGN KEY ("ExamId") REFERENCES "Exams" ("Id") ON DELETE CASCADE;

ALTER TABLE "UserExamResults" ADD CONSTRAINT "FK_UserExamResults_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240210161052_update foreign key2', '6.0.22');

COMMIT;

START TRANSACTION;

ALTER TABLE "ExamTestAnswers" DROP CONSTRAINT "FK_ExamTestAnswers_UserExamResults_UserExamResultId";

UPDATE "ExamTestAnswers" SET "UserExamResultId" = 0 WHERE "UserExamResultId" IS NULL;
ALTER TABLE "ExamTestAnswers" ALTER COLUMN "UserExamResultId" SET NOT NULL;
ALTER TABLE "ExamTestAnswers" ALTER COLUMN "UserExamResultId" SET DEFAULT 0;

ALTER TABLE "ExamTestAnswers" ADD CONSTRAINT "FK_ExamTestAnswers_UserExamResults_UserExamResultId" FOREIGN KEY ("UserExamResultId") REFERENCES "UserExamResults" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240210162450_update foreign key3', '6.0.22');

COMMIT;