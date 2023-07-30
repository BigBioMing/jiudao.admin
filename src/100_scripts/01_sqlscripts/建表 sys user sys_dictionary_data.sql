CREATE TABLE `sys_dictionary_data`(
	`Id` bigint NOT NULL COMMENT '主键',
	`Code` varchar(20) NOT NULL COMMENT '编码',
	`Name` varchar(30) NOT NULL COMMENT '名称',
	`DictionaryDefineId` bigint NOT NULL COMMENT '字典定义表ID',
	`ParentId` bigint NOT NULL COMMENT '上级ID',
	`Sort` int(4) NOT NULL DEFAULT 0 COMMENT '序号 越大越靠前',
	`Description` varchar(200) NULL DEFAULT NULL COMMENT '描述',
	`Enabled` int(1) NOT NULL DEFAULT 0 COMMENT '是否启用 0-禁用 1-启用',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` varchar(30) NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` varchar(30) NOT NULL COMMENT '修改时间',
	PRIMARY KEY (`Id`) USING BTREE,
	KEY `idx_Code`(`Code`)  USING BTREE
) COMMENT '字典定义表';