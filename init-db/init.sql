SELECT 'CREATE DATABASE users_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'users_db')\gexec

SELECT 'CREATE DATABASE billing_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'billing_db')\gexec

SELECT 'CREATE DATABASE gameplay_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'gameplay_db')\gexec