/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50639
Source Host           : 192.168.0.151:3306
Source Database       : tour

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2018-08-11 15:32:16
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_user_address
-- ----------------------------
DROP TABLE IF EXISTS `t_user_address`;
CREATE TABLE `t_user_address` (
  `id` varchar(18) NOT NULL DEFAULT '' COMMENT '主键',
  `user_id` varchar(18) DEFAULT NULL COMMENT '用户id',
  `user_name` varchar(50) DEFAULT NULL COMMENT '用户姓名',
  `mobile` varchar(255) DEFAULT NULL COMMENT '电话',
  `region` varchar(255) DEFAULT NULL COMMENT '所在地区',
  `address` varchar(255) DEFAULT NULL COMMENT '所在地址',
  `zip_code` varchar(255) DEFAULT NULL COMMENT '邮政编码',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user_address
-- ----------------------------
INSERT INTO `t_user_address` VALUES ('474254069498777600', '459370099799887872', '诱惑', '17521552664', '闵行区', '上海市闵行区雅致路228弄', '123456');
INSERT INTO `t_user_address` VALUES ('474636606087761920', '464747873209290752', '啦啦啦', '17521552664', '上海', '上海市', '422220');
INSERT INTO `t_user_address` VALUES ('474636709699653632', '464747873209290752', '诱惑', '17521552664', '北京', '北京市', '422220');
SET FOREIGN_KEY_CHECKS=1;
