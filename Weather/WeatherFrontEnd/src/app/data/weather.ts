import { Guid } from "guid-typescript";
import { IsActive } from "./is-active";

export interface Weather {
    id: Guid;
    temperature: number;
    humidity: number;
    createAt: Date;
    updatedAt: Date;
    isActive: IsActive;
  }