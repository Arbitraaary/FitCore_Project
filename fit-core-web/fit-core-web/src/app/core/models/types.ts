// ─── Enums ───────────────────────────────────────────────────────────────────

export type UserType = 'admin' | 'manager' | 'coach' | 'client';

export type SpecializationType = 'Box' | 'Karate' | 'Swim';

export type EquipmentType = 'dumbbells' | 'barbells' | 'treadmills';

export type RoomType = 'SPA' | 'pool' | 'hammam';

export type MembershipStatus = 'active' | 'expired' | 'suspended';

// ─── Entities ────────────────────────────────────────────────────────────────

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  userType: UserType;
  passwordHash?: string;
  passwordSalt?: string;
}

export interface Coach extends User {
  userType: 'coach';
  specialization: SpecializationType;
}

export interface Client extends User {
  userType: 'client';
}

export interface Manager extends User {
  userType: 'manager';
  locationId: string;
}

export interface Admin extends User {
  userType: 'admin';
}

export interface Location {
  id: string;
  name: string;
  address: string;
}

export interface Room {
  id: string;
  locationId: string;
  roomType: RoomType;
  capacity: number;
}

export interface Equipment {
  id: string;
  locationId: string;
  equipmentType: EquipmentType;
  quantity: number;
}

export interface MembershipType {
  id: string;
  name: string;
  description: string;
  duration: number; // days
  price: number;
}

export interface ClientMembership {
  id: string;
  membershipTypeId: string;
  clientId: string;
  startDate: string; // ISO date
  endDate: string; // ISO date
  status: MembershipStatus;
}

export interface PersonalTrainingSession {
  id: string;
  clientId: string;
  coachId: string;
  roomId: string;
  name: string;
  startTime: string; // ISO datetime
  endTime: string; // ISO datetime
}

export interface GroupTrainingSession {
  id: string;
  coachId: string;
  roomId: string;
  name: string;
  description: string;
  capacity: number;
  startTime: string;
  endTime: string;
  enrolledClientIds: string[];
}

// ─── Auth ────────────────────────────────────────────────────────────────────

export interface AuthUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: 'manager' | 'coach';
  locationId?: string; // present for manager
  coachId?: string; // present for coach (same as id)
}
