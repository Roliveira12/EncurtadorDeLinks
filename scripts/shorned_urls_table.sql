CREATE TABLE IF NOT EXISTS Shortenedurls (
    "Id" bigint PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    "OriginalUrl" VARCHAR(255) NOT NULL,
    "ShorterUrlId" VARCHAR(50) NOT NULL,
    "AccessCount" bigint NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL
);