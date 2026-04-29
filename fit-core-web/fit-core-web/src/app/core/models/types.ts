// ─── Enums ───────────────────────────────────────────────────────────────────

export type UserType = 'admin' | 'manager' | 'coach' | 'client';

export type SpecializationType = 'Box' | 'Karate' | 'Swim' | 'Dance' | 'Yoga' | 'Stretching';

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
  location: GymLocation;
}


export interface CoachWithSessionCount extends Coach {
  sessionCount: number;
}

export interface Client extends User {
  userType: 'client';
  activeMembership?: MembershipType;
}

export interface Manager extends User {
  userType: 'manager';
  location: GymLocation;
}

export interface Admin extends User {
  userType: 'admin';
}

export interface GymLocation {
  name: string;
  address: string;
}

export interface Room {
  id: string;
  locationName: string;
  roomType: RoomType;
  capacity: number;
}

export interface Equipment {
  id: string;
  locationName: string;
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
  coachName: string;
  roomId: string;
  name: string;
  startTime: Date; // ISO datetime
  endTime: Date; // ISO datetime
  type: string;
}

export interface GroupTrainingSession {
  id: string;
  coachId: string;
  coachName: string;
  roomId: string;
  name: string;
  description: string;
  capacity: number;
  startTime: Date;
  endTime: Date;
  enrolledClientIds: string[];
  type: string;
}

export interface GroupTrainingSessionWithCoachAndRoom extends GroupTrainingSession {
  coach: Coach,
  room: Room,
}

export interface PersonalTrainingSessionWithCoachAndRoom extends PersonalTrainingSession {
  coach: Coach,
  room: Room,
}

// ─── Auth ────────────────────────────────────────────────────────────────────

export interface AuthUser {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: UserType;
  phoneNumber: string;
  locationName?: string; // present for manager
  coachId?: string; // present for coach (same as id)
}
