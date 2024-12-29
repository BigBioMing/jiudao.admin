DROP TABLE IF EXISTS `sys_route_resource`;
CREATE TABLE `sys_route_resource`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Code` varchar(120) DEFAULT NULL COMMENT '编码',
  `Name` varchar(120) DEFAULT NULL COMMENT '名称',
  `Title` varchar(120) DEFAULT NULL COMMENT '标题',
  `Url` varchar(120) DEFAULT NULL COMMENT 'Url',
  `Redirect` varchar(120) DEFAULT NULL COMMENT '重定向地址',
  `Component` varchar(120) DEFAULT NULL COMMENT '组件路径（当前路径对应的组件，或者默认组件：BasicLayout）',
  `ParentId` bigint(20) NOT NULL COMMENT '父级菜单Id',
  `Icon` varchar(120) DEFAULT NULL COMMENT '图标',
  `ShowInMenu` tinyint(1) NOT NULL COMMENT '是否在菜单中显示（true-显示 false-不显示）',
  `IsThird` tinyint(1) NOT NULL COMMENT '是否是外部网页（true-是 false-不是）',
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
) COMMENT '路由资源表';