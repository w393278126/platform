/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50639
Source Host           : 192.168.0.151:3306
Source Database       : tour

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2018-08-11 15:32:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_user_invoicetitle
-- ----------------------------
DROP TABLE IF EXISTS `t_user_invoicetitle`;
CREATE TABLE `t_user_invoicetitle` (
  `id` varchar(18) NOT NULL DEFAULT '' COMMENT '主键',
  `userID` varchar(18) NOT NULL COMMENT '用户ID',
  `type` tinyint(1) NOT NULL COMMENT '1、企业单位 2、个人',
  `title` varchar(50) CHARACTER SET utf8mb4 NOT NULL COMMENT '发票标题',
  `num` varchar(30) CHARACTER SET utf8mb4 NOT NULL COMMENT '税号',
  `isNormal` tinyint(1) NOT NULL COMMENT '1、默认 2、非默认',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user_invoicetitle
-- ----------------------------
INSERT INTO `t_user_invoicetitle` VALUES ('471337956616048640', '459370099799887872', '1', '**公司', '12345678952445', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471338347957194752', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471338734680412160', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471338782654861312', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471339226412224512', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471339479609774080', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471339691925442560', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471340274711400448', '459370099799887872', '1', '****有限公司', '1234567890415', '2');
INSERT INTO `t_user_invoicetitle` VALUES ('471340562155442176', '459370099799887872', '1', '****有限公司', '1234567890415', '1');
SET FOREIGN_KEY_CHECKS=1;
