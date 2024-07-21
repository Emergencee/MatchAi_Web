package com.matchai.board.service;

import javax.servlet.http.HttpSession;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


public interface BoardService {
	// kbo 당일 경기 수
	int kboCnt();
	
	// mlb 당일 경기 수
	int mlbCnt();

	// kbo 경기목록
	List<HashMap<String, Object>> kboMatchList();

	// mlb 경기목록
	List<HashMap<String, Object>> mlbMatchList();

	// 게임예측 데이터
	HashMap<String, Object> fetchGameData(String code1, String code2);

	HashMap<String, Object> aiData(String matchcode);

	int searchBoard(String matchcode);

	void insertAiData(HashMap<String, Object> aiData);

	void insertComment(HashMap<String,Object>map);

    int getBoardNumber(String matchcode);

	List<HashMap<String, Object>> getCommentList(String brdno);
}
