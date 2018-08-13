/*
Navicat MySQL Data Transfer

Source Server         : dev
Source Server Version : 50639
Source Host           : 192.168.0.151:3306
Source Database       : tour

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2018-08-11 15:30:10
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_order_orderhotel
-- ----------------------------
DROP TABLE IF EXISTS `t_order_orderhotel`;
CREATE TABLE `t_order_orderhotel` (
  `id` varchar(18) NOT NULL,
  `userID` varchar(18) DEFAULT NULL,
  `orderID` varchar(18) DEFAULT NULL,
  `bookingID` varchar(32) DEFAULT NULL,
  `bookingStates` varchar(10) DEFAULT NULL,
  `states` int(2) DEFAULT NULL,
  `cityID` varchar(10) DEFAULT NULL,
  `cityName` varchar(50) DEFAULT NULL,
  `hotelID` varchar(30) DEFAULT NULL,
  `hotelName` varchar(50) DEFAULT NULL,
  `hotelPhone` varchar(30) DEFAULT NULL,
  `checkInDate` varchar(15) DEFAULT NULL,
  `checkOutDate` varchar(15) DEFAULT NULL,
  `nights` int(5) DEFAULT NULL,
  `roomName` varchar(30) DEFAULT NULL,
  `breakfast` varchar(50) DEFAULT NULL,
  `nationality` varchar(10) DEFAULT NULL,
  `adult` varchar(5) DEFAULT NULL,
  `children` varchar(5) DEFAULT NULL,
  `childrenAge` varchar(10) DEFAULT NULL,
  `rateCode` varchar(32) DEFAULT NULL,
  `roomCount` varchar(1) DEFAULT NULL,
  `currency` varchar(6) DEFAULT NULL,
  `totalAmount` decimal(10,1) DEFAULT NULL,
  `name` varchar(30) DEFAULT NULL,
  `phone` varchar(13) DEFAULT NULL,
  `email` varchar(30) DEFAULT NULL,
  `guestRemarks` varchar(50) DEFAULT NULL,
  `rooms` varchar(100) DEFAULT NULL,
  `addDate` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `channel` int(1) DEFAULT NULL,
  `confirmCode` varchar(50) DEFAULT NULL,
  `cancelReason` varchar(100) DEFAULT NULL,
  `cancelAmount` decimal(10,2) DEFAULT '0.00',
  `costPrice` varchar(20) DEFAULT '0.00',
  `credit` varchar(20) DEFAULT NULL COMMENT '授信余额',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_order_orderhotel
-- ----------------------------
SET FOREIGN_KEY_CHECKS=1;
