package com.matchai.board;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.scheduling.annotation.EnableScheduling;

@SpringBootApplication
@MapperScan({"com.matchai.board.mapper", "com.game.baseball.api.mapper"})
@ComponentScan({"com.matchai.board", "com.game.baseball.api"})
@EnableScheduling // 스케줄링 활성화
public class MatchaiApplication {

	public static void main(String[] args) {
		SpringApplication.run(MatchaiApplication.class, args);
	}

}
