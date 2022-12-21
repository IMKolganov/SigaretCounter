CREATE SEQUENCE counterSigaretsId_seq;
ALTER TABLE "CounterSigarets" ALTER COLUMN "id" SET DEFAULT nextval('counterSigaretsId_seq');
ALTER SEQUENCE counterSigaretsId_seq RESTART WITH 4