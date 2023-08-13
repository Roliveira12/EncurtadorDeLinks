CREATE TABLE IF NOT EXISTS Shortenedurls (
    id serial PRIMARY KEY,
    original_url VARCHAR(255) NOT NULL,
    shorter_url_id VARCHAR(50) NOT NULL,
    access_count bigint NOT NULL,
    created_date timestamp with time zone NOT NULL
);