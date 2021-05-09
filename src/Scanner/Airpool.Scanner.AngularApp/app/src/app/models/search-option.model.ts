export interface SearchOption {
    id: SearchOptionEnum;
    name: string;
}

export enum SearchOptionEnum {
    Default = 1,
    FromCache = 2,
    Greedy = 3
}