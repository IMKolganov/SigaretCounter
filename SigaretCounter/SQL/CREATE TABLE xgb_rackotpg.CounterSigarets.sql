-- xgb_rackotpg."CounterSigarets" definition

-- Drop table

-- DROP TABLE xgb_rackotpg."CounterSigarets";

CREATE TABLE xgb_rackotpg."CounterSigarets" (
	id int4 NOT NULL DEFAULT 0,
	userid int4 NOT NULL DEFAULT 0,
	"SigaretsCount" int4 NOT NULL DEFAULT 0,
	"CurrentDate" timestamp NOT NULL,
	CONSTRAINT "CounterSigarets_pkey" PRIMARY KEY (id)
);