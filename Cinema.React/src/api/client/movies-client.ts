import { MovieResponseDto } from "@/api/models/MovieResponseDto";

export async function getMovies(count?: number): Promise<MovieResponseDto[]> {
    throw new Error("Not implemented");
}

export async function getMovie(id: number): Promise<MovieResponseDto> {
    throw new Error("Not implemented");
}