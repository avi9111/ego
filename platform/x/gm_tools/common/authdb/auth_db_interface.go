package authdb

import (
	"taiyouxi/platform/planx/util/logs"
	authCfg "taiyouxi/platform/x/auth/config"
	"taiyouxi/platform/x/auth/models"
	"taiyouxi/platform/x/gm_tools/config"
)

var (
	authDB models.DBInterface
)

func Init(cfg config.CommonConfig) {
	authCfg.Cfg.Dynamo_NameDevice = cfg.AuthDynamoDBDevice
	authCfg.Cfg.Dynamo_NameName = cfg.AuthDynamoDBName
	authCfg.Cfg.Dynamo_NameUserInfo = cfg.AuthDynamoDBUserInfo
	logs.Info("gmtools init auth models %v", cfg)
	switch cfg.AuthDBDriver {
	case "MongoDB":
		authDB = &models.DBByMongoDB{}
		authDB.Init(models.DBConfig{
			MongoDBName: cfg.AuthMongoDBName,
			MongoDBUrl:  cfg.AuthMongoDBUrl,
		})
	case "DynamoDB":
		fallthrough
	default:
		authDB = &models.DBByDynamoDB{}
		authDB.Init(models.DBConfig{
			DynamoRegion:          cfg.AWS_Region,
			DynamoAccessKeyID:     cfg.AWS_AccessKey,
			DynamoSecretAccessKey: cfg.AWS_SecretKey,
			DynamoSessionToken:    "",
		})
	}

}

func GetDB() models.DBInterface {
	return authDB
}
