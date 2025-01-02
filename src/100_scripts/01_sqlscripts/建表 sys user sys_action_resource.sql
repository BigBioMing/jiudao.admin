DROP TABLE IF EXISTS `sys_action_resource`;
CREATE TABLE `sys_action_resource`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Code` varchar(120) DEFAULT NULL COMMENT '编码',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `RouteResourceId` bigint(20) NOT NULL COMMENT '路由资源表ID',
  `Sort` tinyint(2) NOT NULL DEFAULT 0 COMMENT '顺序（正序排序）',
	`Enabled` bigint NOT NULL DEFAULT 0 COMMENT '是否启用（关联字段表）',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` datetime NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` datetime NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_Code`(`Code`)  USING BTREE
) COMMENT '操作（按钮）资源表';