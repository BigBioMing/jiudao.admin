CREATE TABLE `sys_route_resource`(
	`Id` bigint NOT NULL COMMENT '主键',
	`Code` varchar(20) NOT NULL COMMENT '编码',
	`Name` varchar(30) NOT NULL COMMENT '名称',
	`Enabled` bigint NOT NULL DEFAULT 0 COMMENT '是否启用（关联字段表）',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` varchar(30) NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` varchar(30) NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_Code`(`Code`)  USING BTREE
) COMMENT '路由资源表';