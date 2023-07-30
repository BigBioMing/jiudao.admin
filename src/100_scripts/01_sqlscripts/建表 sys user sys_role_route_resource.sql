CREATE TABLE `sys_role_route_resource`(
	`Id` bigint NOT NULL COMMENT '主键',
	`RoleId` bigint NOT NULL COMMENT '角色ID',
	`RouteResourceId` bigint NOT NULL COMMENT '路由资源表ID',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` varchar(30) NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` varchar(30) NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_RoleId`(`RoleId`) USING BTREE,
	KEY `idx_RouteResourceId`(`RouteResourceId`) USING BTREE
) COMMENT '角色表与路由资源表的关系表';