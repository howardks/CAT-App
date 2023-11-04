-- Up

CREATE TABLE USER (
    id INTEGER PRIMARY KEY, 
    username STRING NOT NULL, 
    password STRING NOT NULL,
    balance DOUBLE NOT NULL DEFAULT 0.0
);

-- Down

DROP TABLE USER;
