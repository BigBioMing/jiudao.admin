DROP TABLE IF EXISTS `sys_organization`;
CREATE TABLE `sys_organization`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
	`Code` varchar(20) NOT NULL COMMENT '编码',
	`Name` varchar(30) NOT NULL COMMENT '名称',
	`ParentId` bigint NOT NULL COMMENT '上级机构ID',
	`Path` varchar(100) NOT NULL COMMENT 'Path',
	`Type` bigint NOT NULL DEFAULT 0 COMMENT '类型',
	`Status` bigint NOT NULL DEFAULT 0 COMMENT '状态',
	`Enabled` bigint NOT NULL DEFAULT 0 COMMENT '是否启用（关联字段表）',
	`Description` varchar(200) NULL DEFAULT NULL COMMENT '描述',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` datetime NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` datetime NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_Code`(`Code`)  USING BTREE
) COMMENT '组织机构表';