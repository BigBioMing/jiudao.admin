DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
	`UserId` bigint NOT NULL COMMENT '用户ID',
	`RoleId` bigint NOT NULL COMMENT '角色ID',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` datetime NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` datetime NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_UserId`(`UserId`) USING BTREE,
	KEY `idx_RoleId`(`RoleId`) USING BTREE
) COMMENT '用户表与角色表的关系表';