/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50639
Source Host           : 192.168.0.151:3306
Source Database       : tour

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2018-08-11 15:30:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_order_orderticket
-- ----------------------------
DROP TABLE IF EXISTS `t_order_orderticket`;
CREATE TABLE `t_order_orderticket` (
  `id` varchar(18) NOT NULL,
  `userId` varchar(32) DEFAULT NULL,
  `orderId` varchar(18) DEFAULT NULL COMMENT '主订单id',
  `thirdSerialId` varchar(32) DEFAULT NULL COMMENT '第三方流水id',
  `states` int(2) DEFAULT NULL,
  `sceneryName` varchar(60) DEFAULT NULL COMMENT '景点名称',
  `ticketName` varchar(60) DEFAULT NULL COMMENT '票名',
  `ticketId` varchar(12) DEFAULT NULL COMMENT '门票ID',
  `ticketsNum` int(10) DEFAULT NULL COMMENT '票数',
  `travelDate` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '旅游日期',
  `consumersType` varchar(10) DEFAULT NULL COMMENT '针对人型',
  `screeningId` varchar(18) DEFAULT NULL COMMENT '场次',
  `screeningBeginTime` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `screeningEndTime` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `tcAmount` decimal(10,2) DEFAULT NULL COMMENT '价格',
  `totalPrice` decimal(10,2) DEFAULT NULL COMMENT '结算总价',
  `isRealName` bigint(2) DEFAULT NULL COMMENT '是否是实名制',
  `createTime` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_order_orderticket
-- ----------------------------
INSERT INTO `t_order_orderticket` VALUES ('469804835638743042', null, '469804835638743040', 'sz5b514174164aadc95236004', '1', null, null, '474733', '1', null, null, null, null, null, '9.00', null, '1', '2018-07-20 09:57:15');
INSERT INTO `t_order_orderticket` VALUES ('469806727882215426', '467714558400794624', '469806727882215424', 'sz5b514336164aadc43836002', '1', null, '测试票(北京渔阳滑雪场)', '474733', '1', '2018-12-18 00:00:00', null, null, null, null, '9.00', null, '1', '2018-07-20 10:04:46');
INSERT INTO `t_order_orderticket` VALUES ('469812870293622785', '467714558400794624', '469812870289428480', 'sz5b5148dd164aadc22636008', '1', null, '测试票(北京渔阳滑雪场)', '474733', '1', '2018-12-18 00:00:00', null, null, null, null, '9.00', null, '1', '2018-07-20 10:29:41');
SET FOREIGN_KEY_CHECKS=1;
