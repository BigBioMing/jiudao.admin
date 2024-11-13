DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
	`Account` varchar(30) NOT NULL COMMENT '账号',
	`Name` varchar(30) NOT NULL COMMENT '名称',
	`Mobile` varchar(30) NULL DEFAULT NULL COMMENT '手机号码',
	`Gender` bigint NOT NULL DEFAULT 0 COMMENT '性别（关联字段表）',
	`Email` varchar(30) NULL DEFAULT NULL COMMENT '邮箱',
	`Password` varchar(20) NOT NULL COMMENT '密码',
	`PasswordSalt` varchar(40) NOT NULL COMMENT '密码盐',
	`Enabled` bigint NOT NULL DEFAULT 0 COMMENT '是否启用（关联字段表）',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` datetime NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` datetime NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_Account`(`Account`) USING BTREE
) COMMENT '系统用户表';