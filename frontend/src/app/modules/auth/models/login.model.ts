export interface LoginRequest {
    email:string
    password:string
}

export interface LoginResponse {
    accessToken:string
}

export interface UserResponse {
    id:string
    email:string
    userName:string
    roles:string[]
}