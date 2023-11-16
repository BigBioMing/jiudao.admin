DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE `sys_log`(
	`Id` bigint NOT NULL AUTO_INCREMENT COMMENT '主键',
	`Host` varchar(50) NOT NULL COMMENT 'Host',
	`Url` varchar(100) NOT NULL COMMENT 'Url',
	`Method` varchar(8) NOT NULL COMMENT '请求谓词',
	`StartTime` datetime NOT NULL COMMENT '开始时间',
	`EndTime` datetime NOT NULL COMMENT '结束时间',
	`Cost` decimal(18,2) NOT NULL DEFAULT -1 COMMENT '请求耗时',
	`Headers` varchar(1000) NOT NULL DEFAULT '' COMMENT 'Headers',
	`RequestParams` varchar(500) NOT NULL DEFAULT '' COMMENT '请求参数',
	`ResponseParams` varchar(500) NOT NULL DEFAULT '' COMMENT '响应参数',
	`IsDeleted` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否删除 0-正常 1-已删除',
	`CreateId` bigint NOT NULL COMMENT '创建人Id',
	`CreateSource` varchar(30) NOT NULL COMMENT '创建人',
	`CreateDate` datetime NOT NULL COMMENT '创建时间',
	`UpdateId` bigint NOT NULL COMMENT '修改人Id',
	`UpdateSource` varchar(30) NOT NULL COMMENT '修改人',
	`UpdateDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '最后修改时间',
	PRIMARY KEY (`Id`) USING BTREE
) COMMENT 'HTTP请求响应日志表';