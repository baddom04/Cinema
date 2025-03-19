import { ScreeningResponseDto } from "@/api/models/ScreeningResponseDto";
import { SeatResponseDto } from "@/api/models/SeatResponseDto";

export async function getTodaysScreenings(): Promise<ScreeningResponseDto[]> {
    throw new Error("Not implemented");
}

export async function getFutureScreenings(movieId: number): Promise<ScreeningResponseDto[]> {
    throw new Error("Not implemented");
}

export async function getScreeningById(id: number): Promise<ScreeningResponseDto> {
    throw new Error("Not implemented");
}

export async function getSeatsForScreening(id: number): Promise<SeatResponseDto[]> {
    throw new Error("Not implemented");
}